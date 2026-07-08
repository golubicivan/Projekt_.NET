using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

var builder = WebApplication.CreateBuilder(args);

// ============= Serilog logging (konzola + rolling file) =============
// Datoteke: logs/gdjecemo-YYYYMMDD.log, zadrzava zadnjih 14 dana.
builder.Host.UseSerilog((context, config) => config
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: Path.Combine(context.HostingEnvironment.ContentRootPath, "logs", "gdjecemo-.log"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}"));

builder.Services.AddControllersWithViews(options =>
{
    // Fix: decimalni brojevi iz formi (tocka I zarez rade neovisno o hr kulturi)
    options.ModelBinderProviders.Insert(0, new ZagrebEvents.Web.Services.InvariantNumberModelBinderProvider());
    // Fix: prazan string ostaje "" (ne null) -> opcionalna string polja ne padaju na implicitnom Required
    options.ModelMetadataDetailsProviders.Add(new ZagrebEvents.Web.Services.EmptyStringMetadataProvider());
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

// Email obavijesti (SMTP iz konfiguracije; bez SMTP-a sprema u App_Data/emails)
builder.Services.AddScoped<ZagrebEvents.Web.Services.IEmailService, ZagrebEvents.Web.Services.EmailService>();

// AI unos podataka (Claude API; kljuc u user-secrets Anthropic:ApiKey)
builder.Services.AddSingleton<ZagrebEvents.Web.Services.IAiEventService, ZagrebEvents.Web.Services.AiEventService>();

// EF DbContext (Dependency Injection)
builder.Services.AddDbContext<ZagrebEventsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ZagrebEventsDbContext"),
        opt => opt.MigrationsAssembly("ZagrebEvents.DAL")));

// ============= ASP.NET Core Identity =============
builder.Services
    .AddIdentity<AppUser, IdentityRole>(options =>
    {
        // Relaksirana pravila lozinke za prototip (demo nalozi: ivan123, admin123...)
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ZagrebEventsDbContext>()
    .AddClaimsPrincipalFactory<ZagrebEvents.Web.Services.AppUserClaimsPrincipalFactory>()
    .AddDefaultTokenProviders();

// Identity cookie putanje
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);

    // Za API rute vraćaj 401/403 umjesto redirecta na login (API klijenti očekuju status kod)
    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }
        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

// ============= Google OAuth (3rd party login) =============
var googleClientId = builder.Configuration["Authentication:Google:ClientId"];
var googleClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
if (!string.IsNullOrEmpty(googleClientId) && !string.IsNullOrEmpty(googleClientSecret))
{
    builder.Services.AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = googleClientId;
            options.ClientSecret = googleClientSecret;
        });
}

builder.Services.AddAuthorization();

var app = builder.Build();

// ============= Seed Identity (role + nalozi) pri startu =============
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<ZagrebEventsDbContext>();

    // Migracije samo za relacijske baze (InMemory u testovima ih ne podržava)
    if (db.Database.IsRelational())
    {
        db.Database.Migrate();

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await IdentitySeeder.SeedAsync(db, userManager, roleManager);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
    app.UseHsts();
}

// Lokalizacija (hr + en)
var supportedCultures = new[]
{
    new CultureInfo("hr"),
    new CultureInfo("en-US")
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("hr"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

// HTTP request logging (metoda, ruta, status, trajanje) u Serilog sinkove
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   // PRIJE UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Potrebno za integracijske testove (WebApplicationFactory<Program>)
public partial class Program { }
