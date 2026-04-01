namespace ZagrebEvents.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegisteredAt { get; set; }

        // 1-N: Korisnik moze imati vise rezervacija
        public List<Reservation> Reservations { get; set; }

        // 1-N: Korisnik moze ostaviti vise recenzija
        public List<Review> Reviews { get; set; }

        // N-N: Korisnik moze imati vise omiljenih lokacija (favorites)
        public List<Venue> FavoriteVenues { get; set; }

        public User()
        {
            Reservations = new List<Reservation>();
            Reviews = new List<Review>();
            FavoriteVenues = new List<Venue>();
        }

        public string FullName => $"{FirstName} {LastName}";

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

        public bool IsAdult => Age >= 18;
    }
}
