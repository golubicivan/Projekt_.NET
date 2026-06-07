using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ZagrebEvents.Model
{
    // AppUser je ASP.NET Core Identity korisnik (autentikacija + role).
    // Odvojen je od domenskog User modela (rezervacije, recenzije).
    // Povezuju se preko User.AppUserId (Opcija B iz Lab 5 dogovora).
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "OIB mora imati točno 11 znamenki.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "OIB smije sadržavati samo brojeve.")]
        public string OIB { get; set; } = "";

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora imati točno 13 znamenki.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "JMBG smije sadržavati samo brojeve.")]
        public string JMBG { get; set; } = "";

        // Veza na domenski profil je na User.AppUserId strani (jedan smjer).
    }
}
