using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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
