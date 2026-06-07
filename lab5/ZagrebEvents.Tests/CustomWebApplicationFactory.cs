using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Tests
{
    // Pokreće stvarnu aplikaciju, ali zamjenjuje SQL Server bazu s EF InMemory bazom
    // i dodaje testni auth scheme. Svaka instanca dobiva svoju izoliranu bazu.
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _dbName = "TestDb_" + Guid.NewGuid();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");

            builder.ConfigureServices(services =>
            {
                // Ukloni postojeću (SQL Server) DbContext registraciju
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ZagrebEventsDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                // Dodaj InMemory bazu
                services.AddDbContext<ZagrebEventsDbContext>(options =>
                    options.UseInMemoryDatabase(_dbName));

                // Testni auth scheme kao default (autorizacija radi preko X-Test-Roles headera)
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
                    options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
                    options.DefaultScheme = TestAuthHandler.SchemeName;
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });
            });
        }

        // Pristup DbContextu u testovima za pripremu (seed) podataka
        public ZagrebEventsDbContext CreateDbContext()
        {
            var scope = Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<ZagrebEventsDbContext>();
        }
    }
}
