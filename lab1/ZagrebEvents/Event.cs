namespace ZagrebEvents.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EventType Type { get; set; }
        public decimal EntryPrice { get; set; }
        public string PosterUrl { get; set; }
        public int AgeLimit { get; set; }

        // N-1: Event pripada jednoj lokaciji
        public Venue Venue { get; set; }

        // 1-N: Event ima vise rezervacija
        public List<Reservation> Reservations { get; set; }

        // 1-N: Event ima vise recenzija
        public List<Review> Reviews { get; set; }

        public Event()
        {
            Reservations = new List<Reservation>();
            Reviews = new List<Review>();
        }

        public double AverageRating
        {
            get
            {
                if (!Reviews.Any()) return 0;
                return Reviews.Average(r => r.Rating);
            }
        }

        public bool IsUpcoming => StartTime > DateTime.Now;
        public bool IsActive => StartTime <= DateTime.Now && EndTime >= DateTime.Now;

        public TimeSpan Duration => EndTime - StartTime;
    }
}
