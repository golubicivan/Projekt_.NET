using System.ComponentModel;
using System.Net;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

// ============================================================
// GdjeCemo MCP server (stdio)
// Izlaze aplikacijski API kao MCP alate za agentic IDE
// (Claude Code, Cursor...). Konfiguracija: .mcp.json u rootu repoa.
//
//   ZE_BASEURL      - URL aplikacije (default http://localhost:5053)
//   ZE_ADMIN_EMAIL  - admin za write operacije (default seed admin)
//   ZE_ADMIN_PASS
// ============================================================

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders(); // stdio je rezerviran za MCP protokol
builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();
await builder.Build().RunAsync();

[McpServerToolType]
public static class GdjeCemoTools
{
    private static readonly string BaseUrl =
        Environment.GetEnvironmentVariable("ZE_BASEURL") ?? "http://localhost:5053";
    private static readonly string AdminEmail =
        Environment.GetEnvironmentVariable("ZE_ADMIN_EMAIL") ?? "luka.peric@admin.com";
    private static readonly string AdminPass =
        Environment.GetEnvironmentVariable("ZE_ADMIN_PASS") ?? "admin123";

    private static readonly CookieContainer Cookies = new();
    private static readonly HttpClient Http = new(new HttpClientHandler
    {
        CookieContainer = Cookies,
        AllowAutoRedirect = true
    })
    { BaseAddress = new Uri(BaseUrl) };

    private static bool _loggedIn;

    // ---------- READ ALATI (javni API) ----------

    [McpServerTool(Name = "list_events"), Description(
        "Vrati sve nadolazece evente u Zagrebu (JSON). Opcionalno filtriraj tekstom (naziv eventa ili lokacije).")]
    public static async Task<string> ListEvents(
        [Description("Tekst pretrage, npr. 'techno' ili 'h2o' (opcionalno)")] string? q = null)
        => await Get($"/api/events{Query(q)}");

    [McpServerTool(Name = "get_event"), Description("Vrati detalje jednog eventa po ID-u (JSON).")]
    public static async Task<string> GetEvent([Description("ID eventa")] int id)
        => await Get($"/api/events/{id}");

    [McpServerTool(Name = "list_venues"), Description(
        "Vrati sve lokacije/klubove (JSON). Opcionalno filtriraj tekstom (naziv ili adresa).")]
    public static async Task<string> ListVenues(
        [Description("Tekst pretrage, npr. 'aquarius' (opcionalno)")] string? q = null)
        => await Get($"/api/venues{Query(q)}");

    [McpServerTool(Name = "get_venue"), Description("Vrati detalje jedne lokacije po ID-u (JSON).")]
    public static async Task<string> GetVenue([Description("ID lokacije")] int id)
        => await Get($"/api/venues/{id}");

    [McpServerTool(Name = "list_tables"), Description(
        "Vrati stolove neke lokacije (JSON): broj stola, kapacitet, zona (0=Regular, 1=VIP).")]
    public static async Task<string> ListTables([Description("ID lokacije (venue)")] int venueId)
    {
        var all = await Get("/api/tables");
        using var doc = System.Text.Json.JsonDocument.Parse(all);
        var filtered = doc.RootElement.EnumerateArray()
            .Where(t => t.TryGetProperty("venueId", out var v) && v.GetInt32() == venueId)
            .Select(t => t.GetRawText());
        return "[" + string.Join(",", filtered) + "]";
    }

    [McpServerTool(Name = "search"), Description(
        "Globalna pretraga lokacija i evenata odjednom. Vraca JSON s 'venues' i 'events' poljima.")]
    public static async Task<string> Search([Description("Pojam pretrage")] string q)
    {
        var venues = await Get($"/api/venues{Query(q)}");
        var events = await Get($"/api/events{Query(q)}");
        return $"{{\"venues\":{venues},\"events\":{events}}}";
    }

    // ---------- WRITE ALATI (admin login preko MVC forme) ----------

    [McpServerTool(Name = "create_event"), Description(
        "Kreiraj novi event (zahtijeva admin prava - server se sam prijavi). " +
        "Vraca kreirani event kao JSON. Datumi u ISO formatu (npr. 2026-08-15T22:00).")]
    public static async Task<string> CreateEvent(
        [Description("Naziv eventa")] string name,
        [Description("ID lokacije (venue) - koristi list_venues za popis")] int venueId,
        [Description("Pocetak, ISO format npr. 2026-08-15T22:00")] string startTime,
        [Description("Kraj, ISO format npr. 2026-08-16T04:00")] string endTime,
        [Description("Tip: 0=DJ noc, 1=Koncert, 2=Pub kviz, 3=Festival")] int type = 0,
        [Description("Cijena ulaza u EUR (0 = besplatno)")] decimal entryPrice = 0,
        [Description("Opis eventa (opcionalno)")] string? description = null,
        [Description("Dobna granica (0 = nema)")] int ageLimit = 0)
    {
        await EnsureAdminAsync();
        var resp = await Http.PostAsJsonAsync("/api/events", new
        {
            name,
            description = description ?? "",
            startTime = DateTime.Parse(startTime, System.Globalization.CultureInfo.InvariantCulture),
            endTime = DateTime.Parse(endTime, System.Globalization.CultureInfo.InvariantCulture),
            type,
            entryPrice,
            posterUrl = "",
            ageLimit,
            venueId
        });
        var text = await resp.Content.ReadAsStringAsync();
        return resp.IsSuccessStatusCode ? text : $"GRESKA {(int)resp.StatusCode}: {text}";
    }

    [McpServerTool(Name = "delete_event"), Description(
        "Obrisi event po ID-u (soft delete; zahtijeva admin prava - server se sam prijavi).")]
    public static async Task<string> DeleteEvent([Description("ID eventa")] int id)
    {
        await EnsureAdminAsync();
        var resp = await Http.DeleteAsync($"/api/events/{id}");
        return resp.IsSuccessStatusCode
            ? $"Event {id} obrisan."
            : $"GRESKA {(int)resp.StatusCode}: {await resp.Content.ReadAsStringAsync()}";
    }

    // ---------- POMOCNE ----------

    private static string Query(string? q) =>
        string.IsNullOrWhiteSpace(q) ? "" : $"?q={Uri.EscapeDataString(q)}";

    private static async Task<string> Get(string path)
    {
        var resp = await Http.GetAsync(path);
        var text = await resp.Content.ReadAsStringAsync();
        return resp.IsSuccessStatusCode ? text : $"GRESKA {(int)resp.StatusCode}: {text}";
    }

    // Admin login preko MVC forme (cookie ostaje u CookieContaineru)
    private static async Task EnsureAdminAsync()
    {
        if (_loggedIn) return;
        var page = await Http.GetStringAsync("/prijava");
        var token = Regex.Match(page, "RequestVerificationToken\"[^>]*value=\"([^\"]+)\"").Groups[1].Value;
        var resp = await Http.PostAsync("/prijava", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["email"] = AdminEmail,
            ["password"] = AdminPass,
            ["__RequestVerificationToken"] = token
        }));
        if (!resp.IsSuccessStatusCode && resp.StatusCode != HttpStatusCode.Redirect)
            throw new Exception($"Admin login nije uspio ({(int)resp.StatusCode}).");
        _loggedIn = true;
    }
}
