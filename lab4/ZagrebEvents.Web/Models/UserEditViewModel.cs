using System.ComponentModel.DataAnnotations;

namespace ZagrebEvents.Web.Models
{
    // Ograničeni ViewModel za User Edit:
    // korisnik može mijenjati SAMO email, telefon i lozinku
    // (NE smije: ime, prezime, datum rođenja, rolu — radi sprječavanja krađe identiteta)
    public class UserEditViewModel
    {
        public int Id { get; set; }

        // Read-only polja (samo za prikaz)
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; }

        // Polja koja se MOGU mijenjati
        [Required(ErrorMessage = "Email je obavezan."), EmailAddress(ErrorMessage = "Email nije u ispravnom formatu.")]
        [MaxLength(120)]
        public string Email { get; set; } = "";

        [MaxLength(30, ErrorMessage = "Telefon je predugačak.")]
        public string PhoneNumber { get; set; } = "";

        // Stara lozinka — obavezno za potvrdu
        [Required(ErrorMessage = "Trenutna lozinka je obavezna za spremanje.")]
        public string CurrentPassword { get; set; } = "";

        // Nova lozinka (opcionalno)
        [MinLength(6, ErrorMessage = "Nova lozinka mora imati barem 6 znakova.")]
        public string? NewPassword { get; set; }

        public string? NewPasswordConfirm { get; set; }
    }
}
