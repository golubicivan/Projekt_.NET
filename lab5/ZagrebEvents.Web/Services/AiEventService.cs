using System.Text.Json;
using Anthropic;
using Anthropic.Models.Messages;
using ClaudeModel = Anthropic.Models.Messages.Model;

namespace ZagrebEvents.Web.Services
{
    // Rezultat AI parsiranja - nacrt eventa ili poruka zasto ne moze
    public class AiEventDraft
    {
        public bool Ok { get; set; }
        public string Error { get; set; } = "";
        public string Name { get; set; } = "";
        public int VenueId { get; set; }
        public string StartTime { get; set; } = "";
        public string EndTime { get; set; } = "";
        public int Type { get; set; }
        public decimal EntryPrice { get; set; }
        public int AgeLimit { get; set; }
        public string Description { get; set; } = "";
    }

    public interface IAiEventService
    {
        bool IsConfigured { get; }
        Task<AiEventDraft> ParseEventAsync(string prompt, IReadOnlyList<(int Id, string Name)> venues);
    }

    // AI unos podataka: prirodni jezik -> strukturirani event (Claude API, strukturirani JSON izlaz)
    public class AiEventService : IAiEventService
    {
        private readonly string? _apiKey;
        private readonly ILogger<AiEventService> _logger;

        public AiEventService(IConfiguration config, ILogger<AiEventService> logger)
        {
            _apiKey = config["Anthropic:ApiKey"];
            _logger = logger;
        }

        public bool IsConfigured => !string.IsNullOrWhiteSpace(_apiKey);

        public async Task<AiEventDraft> ParseEventAsync(string prompt, IReadOnlyList<(int Id, string Name)> venues)
        {
            if (!IsConfigured)
                return new AiEventDraft { Ok = false, Error = "Anthropic API ključ nije konfiguriran (user-secrets: Anthropic:ApiKey)." };

            var now = DateTime.Now;
            var venueList = string.Join("\n", venues.Select(v => $"{v.Id}: {v.Name}"));
            var system =
$@"Ti si asistent za unos podataka u aplikaciju ""GdjeCemo"" (zagrebački noćni život).
Iz korisnikovog opisa na hrvatskom ili engleskom izvuci podatke za NOVI EVENT i vrati ih kao JSON.

Trenutni datum i vrijeme: {now:yyyy-MM-dd HH:mm} ({now:dddd}, lokalno zagrebačko vrijeme).

Dostupne lokacije (venueId: naziv) — venueId MORA biti s ovog popisa:
{venueList}

Pravila:
- Relativne datume (""sutra"", ""sljedeći petak""...) izračunaj iz trenutnog datuma.
- startTime i endTime vrati u formatu yyyy-MM-ddTHH:mm. Ako event ide preko ponoći, endTime je sljedeći dan.
- Ako vrijeme završetka nije navedeno: klupski eventi traju do 04:00, koncerti 3 sata, pub kvizovi 2 sata.
- type: 0=DJ noć, 1=Koncert, 2=Pub kviz, 3=Festival.
- entryPrice u EUR (0 = besplatno ili nije navedeno). ageLimit 0 = nema dobne granice.
- description: kratak privlačan opis na hrvatskom (1-2 rečenice), napiši ga i ako nije zadan.
- Ako lokaciju ne možeš pouzdano prepoznati na popisu, ili upit nije zahtjev za kreiranje eventa:
  postavi ok=false i u error na hrvatskom objasni što nedostaje; ostala polja neka budu prazna/0.";

            AnthropicClient client = new() { ApiKey = _apiKey };

            try
            {
                return await CallClaudeAsync(client, system, prompt);
            }
            catch (Anthropic.Exceptions.AnthropicApiException ex)
            {
                _logger.LogWarning(ex, "AI unos: Anthropic API greška");
                var msg = ex.Message.Contains("credit balance", StringComparison.OrdinalIgnoreCase)
                    ? "Na Anthropic računu nema kredita. Nadoplati na console.anthropic.com → Plans & Billing pa pokušaj ponovno."
                    : ex.Message.Contains("authentication", StringComparison.OrdinalIgnoreCase)
                        ? "Anthropic API ključ nije valjan. Provjeri user-secrets (Anthropic:ApiKey)."
                        : $"Anthropic API greška: {ex.Message}";
                return new AiEventDraft { Ok = false, Error = msg };
            }
        }

        private async Task<AiEventDraft> CallClaudeAsync(AnthropicClient client, string system, string prompt)
        {
            var response = await client.Messages.Create(new MessageCreateParams
            {
                Model = ClaudeModel.ClaudeOpus4_8,
                MaxTokens = 4000,
                Thinking = new ThinkingConfigAdaptive(),
                System = system,
                OutputConfig = new OutputConfig
                {
                    Format = new JsonOutputFormat
                    {
                        Schema = new Dictionary<string, JsonElement>
                        {
                            ["type"] = JsonSerializer.SerializeToElement("object"),
                            ["properties"] = JsonSerializer.SerializeToElement(new
                            {
                                ok = new { type = "boolean", description = "true ako su svi podaci uspješno izvučeni" },
                                error = new { type = "string", description = "Objašnjenje na hrvatskom kad je ok=false, inače prazno" },
                                name = new { type = "string" },
                                venueId = new { type = "integer" },
                                startTime = new { type = "string", description = "yyyy-MM-ddTHH:mm" },
                                endTime = new { type = "string", description = "yyyy-MM-ddTHH:mm" },
                                type = new { type = "integer" },
                                entryPrice = new { type = "number" },
                                ageLimit = new { type = "integer" },
                                description = new { type = "string" }
                            }),
                            ["required"] = JsonSerializer.SerializeToElement(new[]
                            {
                                "ok", "error", "name", "venueId", "startTime",
                                "endTime", "type", "entryPrice", "ageLimit", "description"
                            }),
                            ["additionalProperties"] = JsonSerializer.SerializeToElement(false)
                        }
                    }
                },
                Messages = [new() { Role = Role.User, Content = prompt }]
            });

            var json = response.Content.Select(b => b.Value).OfType<TextBlock>()
                .Select(t => t.Text).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(json))
                return new AiEventDraft { Ok = false, Error = "AI nije vratio odgovor. Pokušaj ponovno." };

            _logger.LogInformation("AI unos: prompt={Prompt} -> {Json}", prompt, json);

            var draft = JsonSerializer.Deserialize<AiEventDraft>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return draft ?? new AiEventDraft { Ok = false, Error = "Neispravan AI odgovor." };
        }
    }
}
