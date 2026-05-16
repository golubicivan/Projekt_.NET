using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(60)]
        public string FirstName { get; set; } = "";

        [Required, MaxLength(60)]
        public string LastName { get; set; } = "";

        public DateTime DateOfBirth { get; set; }

        [Required, MaxLength(120)]
        public string Email { get; set; } = "";

        [MaxLength(30)]
        public string PhoneNumber { get; set; } = "";

        [Required, MaxLength(100)]
        public string Password { get; set; } = "";

        public UserRole Role { get; set; }
        public DateTime RegisteredAt { get; set; }

        // Soft delete
        public DateTime? DeletedAt { get; set; }

        // 1-N: User ima više rezervacija
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // 1-N: User ima više recenzija
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // N-N: User može imati više favorite venue-a, venue može biti u favorites kod više usera
        public virtual ICollection<Venue> FavoriteVenues { get; set; } = new List<Venue>();

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        [NotMapped]
        public bool IsAdult => Age >= 18;

        [NotMapped]
        public string Initials => string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName)
            ? "?" : $"{FirstName[0]}{LastName[0]}".ToUpper();
    }
}
