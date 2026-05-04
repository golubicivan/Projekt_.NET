using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registracija EF DbContext-a (Dependency Injection)
builder.Services.AddDbContext<ZagrebEventsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ZagrebEventsDbContext"),
        opt => opt.MigrationsAssembly("ZagrebEvents.DAL")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
