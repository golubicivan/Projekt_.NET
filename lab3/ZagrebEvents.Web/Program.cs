using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registracija EF DbContext-a (Dependency Injection)
builder.Services.AddDbContext<ZagrebEventsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ZagrebEventsDbContext"),
        opt => opt.MigrationsAssembly("ZagrebEvents.DAL")));

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();   // PRIJE UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
