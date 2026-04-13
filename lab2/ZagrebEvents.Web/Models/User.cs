namespace ZagrebEvents.Web.Models
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
        public List<Reservation> Reservations { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<Venue> FavoriteVenues { get; set; } = new();

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
        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();
    }
}
