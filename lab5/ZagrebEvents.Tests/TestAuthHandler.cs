using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ZagrebEvents.Tests
{
    // Testni auth handler: autentificira zahtjev SAMO ako postoji header "X-Test-Roles".
    // Time možemo testirati i autorizirane (s headerom) i neautorizirane (bez headera) scenarije.
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "Test";

        public TestAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("X-Test-Roles"))
                return Task.FromResult(AuthenticateResult.NoResult());

            var roles = Request.Headers["X-Test-Roles"].ToString();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "test-app-user-id"),
                new Claim(ClaimTypes.Name, "Test Korisnik"),
                new Claim("DomainUserId", "1")
            };
            foreach (var role in roles.Split(',', StringSplitOptions.RemoveEmptyEntries))
                claims.Add(new Claim(ClaimTypes.Role, role.Trim()));

            var identity = new ClaimsIdentity(claims, SchemeName);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, SchemeName);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
