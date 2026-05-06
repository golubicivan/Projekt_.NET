using ZagrebEvents.Web.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Registracija mock repozitorija (Dependency Injection)
builder.Services.AddSingleton<VenueMockRepository>();
builder.Services.AddSingleton<EventMockRepository>();
builder.Services.AddSingleton<UserMockRepository>();
builder.Services.AddSingleton<ReservationMockRepository>();
builder.Services.AddSingleton<ReviewMockRepository>();

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
