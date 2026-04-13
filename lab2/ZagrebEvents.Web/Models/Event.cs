namespace ZagrebEvents.Web.Models
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
        public Venue Venue { get; set; }
        public List<Reservation> Reservations { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();

        public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        public bool IsUpcoming => StartTime > DateTime.Now;
        public bool IsActive => StartTime <= DateTime.Now && EndTime >= DateTime.Now;
        public TimeSpan Duration => EndTime - StartTime;

        public string TypeIcon => Type switch
        {
            EventType.DJNight => "🪩",
            EventType.Concert => "🎤",
            EventType.PubQuiz => "📖",
            EventType.Festival => "🎪",
            _ => "🎵"
        };

        public string TypeColor => Type switch
        {
            EventType.DJNight => "#7c3aed",
            EventType.Concert => "#ef4444",
            EventType.PubQuiz => "#f59e0b",
            EventType.Festival => "#f97316",
            _ => "#7c3aed"
        };

        public string TypeLabel => Type switch
        {
            EventType.DJNight => "DJ Noć",
            EventType.Concert => "Koncert",
            EventType.PubQuiz => "Pub Quiz",
            EventType.Festival => "Festival",
            _ => "Event"
        };
    }
}
