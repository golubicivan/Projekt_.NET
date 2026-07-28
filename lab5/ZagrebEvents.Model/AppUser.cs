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

        // JMBG ostaje u modelu (prosireni AppUser), ali se pri registraciji vise ne trazi.
        [StringLength(13)]
        public string? JMBG { get; set; }

        // Putanja do slike PREDNJE strane osobnog dokumenta (ime, prezime, datum rodjenja).
        [StringLength(500)]
        public string? IdentityDocumentPath { get; set; }

        // Putanja do slike STRAZNJE strane osobnog dokumenta (OIB).
        [StringLength(500)]
        public string? IdentityDocumentBackPath { get; set; }

        // true = AI je (ili admin rucno) potvrdio da se podaci s osobne poklapaju s profilom.
        // Korisnik koji odbije priloziti osobnu ostaje false i ne moze rezervirati stolove
        // na eventima s dobnom granicom u klubovima koji traze potvrdjen identitet.
        public bool IdentityVerified { get; set; }

        // Veza na domenski profil je na User.AppUserId strani (jedan smjer).
    }
}
