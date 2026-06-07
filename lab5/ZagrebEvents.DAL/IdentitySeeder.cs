using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.Model;

namespace ZagrebEvents.DAL
{
    // Runtime seeder za Identity: kreira role, Identity naloge (s hashiranim lozinkama)
    // i povezuje ih s domenskim User profilima. Pokreće se pri startu aplikacije.
    public static class IdentitySeeder
    {
        public record SeedUser(int DomainUserId, string Email, string Password, string Role, string Oib, string Jmbg);

        private static readonly SeedUser[] Users = new[]
        {
            new SeedUser(1, "ivan.golubic@email.com", "ivan123",  "Guest", "12345678901", "1505003331234"),
            new SeedUser(2, "ana.horvat@email.com",   "demo123",  "Guest", "23456789012", "2208001332345"),
            new SeedUser(3, "marko.kovacevic@email.com","demo123", "Owner", "34567890123", "1003990333456"),
            new SeedUser(4, "petra.babic@email.com",   "demo123",  "Guest", "45678901234", "0112000334567"),
            new SeedUser(5, "luka.peric@admin.com",    "admin123", "Admin", "56789012345", "2007985335678"),
            new SeedUser(6, "karlo.novak@email.com",   "demo123",  "Guest", "67890123456", "2509010336789"),
        };

        public static async Task SeedAsync(
            ZagrebEventsDbContext db,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // 1. Role
            string[] roles = { "Admin", "Owner", "Guest" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // 2. Identity nalozi + povezivanje s domenskim User-om
            foreach (var seed in Users)
            {
                var existing = await userManager.FindByEmailAsync(seed.Email);
                if (existing == null)
                {
                    var appUser = new AppUser
                    {
                        UserName = seed.Email,
                        Email = seed.Email,
                        EmailConfirmed = true,
                        OIB = seed.Oib,
                        JMBG = seed.Jmbg
                    };
                    var result = await userManager.CreateAsync(appUser, seed.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(appUser, seed.Role);

                        // Poveži domenski profil s Identity nalogom
                        var domainUser = await db.Users.FirstOrDefaultAsync(u => u.Id == seed.DomainUserId);
                        if (domainUser != null)
                        {
                            domainUser.AppUserId = appUser.Id;
                            await db.SaveChangesAsync();
                        }
                    }
                }
            }

            // 3. Dodijeli vlasništvo nad venueom Owneru (Marko -> Club Culture)
            var marko = await userManager.FindByEmailAsync("marko.kovacevic@email.com");
            if (marko != null)
            {
                var clubCulture = await db.Venues.FirstOrDefaultAsync(v => v.Id == 1);
                if (clubCulture != null && string.IsNullOrEmpty(clubCulture.OwnerAppUserId))
                {
                    clubCulture.OwnerAppUserId = marko.Id;
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
