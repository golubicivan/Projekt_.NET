using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Services
{
    // ============================================================
    // Pravila: tko smije rezervirati stol na kojem eventu.
    //
    //  1. Dobna granica eventa UVIJEK vrijedi — racuna se na datum eventa,
    //     pa maloljetnik koji do tada napuni 18 automatski dobiva pristup.
    //  2. Za evente s dobnom granicom klub moze traziti potvrdjen identitet
    //     (osobna + AI provjera). Klub koji dob provjerava na ulazu moze to
    //     iskljuciti (Venue.IdentityRequired = false).
    //  3. Eventi bez dobne granice otvoreni su svima — i bez prilozene osobne.
    // ============================================================
    public static class ReservationPolicy
    {
        // Dob na odredjeni datum (npr. na dan eventa, ne danas)
        public static int AgeAt(DateTime dateOfBirth, DateTime when)
        {
            int age = when.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > when.Date.AddYears(-age)) age--;
            return age;
        }

        // Rezultat: smije li, zasto ne, i (ako postoji) gumb koji vodi na rjesenje
        public record Result(bool Allowed, string? Reason = null, string? ActionUrl = null, string? ActionLabel = null);

        public static Result Check(Event ev, User? user, bool identityVerified)
        {
            if (user == null)
                return new Result(false, "Za rezervaciju stola prijavi se ili registriraj.", "/prijava", "Prijavi se");

            if (user.DateOfBirth == default)
                return new Result(false, "U profilu nedostaje datum rođenja pa ne možemo provjeriti dobnu granicu.");

            var age = AgeAt(user.DateOfBirth, ev.StartTime);

            // 1) dobna granica eventa (racuna se na dan eventa)
            if (ev.AgeLimit > 0 && age < ev.AgeLimit)
                return new Result(false,
                    $"Ovaj event ima dobnu granicu {ev.AgeLimit}+, a na dan eventa imat ćeš {age} god. " +
                    "Rezervirati možeš stolove na eventima bez dobne granice.",
                    "/eventi", "Pronađi event bez granice");

            // 2) potvrdjen identitet za evente s dobnom granicom (ako ga klub trazi)
            bool venueRequiresId = ev.Venue?.IdentityRequired ?? true;
            if (ev.AgeLimit > 0 && venueRequiresId && !identityVerified)
                return new Result(false,
                    $"Za evente s dobnom granicom {ev.AgeLimit}+ ovaj klub traži potvrđen identitet. " +
                    "Priloži obje strane osobne pa AI provjeri podatke — ili odaberi event bez dobne granice.",
                    "/potvrdi-identitet", "🪪 Potvrdi identitet");

            return new Result(true);
        }
    }
}
