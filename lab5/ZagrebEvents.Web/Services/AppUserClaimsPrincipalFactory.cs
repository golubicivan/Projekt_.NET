using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Services
{
    // Dodaje custom claim "DomainUserId" pri prijavi kako bi kontroleri
    // lako dohvatili domenski User profil (rezervacije, recenzije) iz Identity korisnika.
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, IdentityRole>
    {
        private readonly ZagrebEventsDbContext _db;

        public AppUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options,
            ZagrebEventsDbContext db)
            : base(userManager, roleManager, options)
        {
            _db = db;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var domainUser = await _db.Users.FirstOrDefaultAsync(u => u.AppUserId == user.Id);
            if (domainUser != null)
            {
                identity.AddClaim(new Claim("DomainUserId", domainUser.Id.ToString()));
                identity.AddClaim(new Claim("FullName", domainUser.FullName));
            }

            return identity;
        }
    }
}
