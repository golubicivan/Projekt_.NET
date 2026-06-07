using System.Security.Claims;

namespace ZagrebEvents.Web.Services
{
    public static class ClaimsPrincipalExtensions
    {
        // Vraća ID domenskog User profila (rezervacije, recenzije) prijavljenog korisnika.
        public static int? GetDomainUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirstValue("DomainUserId");
            return int.TryParse(value, out var id) ? id : null;
        }

        // Vraća ID Identity korisnika (AppUser GUID).
        public static string? GetAppUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // Resource-based autorizacija: smije li korisnik upravljati venueom.
        // Admin smije sve. Owner smije samo svoj venue (OwnerAppUserId == njegov AppUser id).
        public static bool CanManageVenue(this ClaimsPrincipal user, string? ownerAppUserId)
        {
            if (user.IsInRole("Admin")) return true;
            if (user.IsInRole("Owner") && !string.IsNullOrEmpty(ownerAppUserId))
                return user.GetAppUserId() == ownerAppUserId;
            return false;
        }
    }
}
