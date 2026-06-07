using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = "";

        [MaxLength(2000)]
        public string Description { get; set; } = "";

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EventType Type { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal EntryPrice { get; set; }

        [MaxLength(500)]
        public string PosterUrl { get; set; } = "";

        public int AgeLimit { get; set; }

        // Soft delete: null = aktivan, popunjen = obrisan
        public DateTime? DeletedAt { get; set; }

        // 1-N: Event pripada jednom Venueu
        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }
        public virtual Venue? Venue { get; set; }

        // 1-N: Event ima više rezervacija
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // 1-N: Event ima više recenzija
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        // 1-N: Event ima više priloženih datoteka
        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

        [NotMapped]
        public double AverageRating => Reviews != null && Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;

        [NotMapped]
        public bool IsUpcoming => StartTime > DateTime.Now;

        [NotMapped]
        public bool IsActive => StartTime <= DateTime.Now && EndTime >= DateTime.Now;

        [NotMapped]
        public TimeSpan Duration => EndTime - StartTime;

        [NotMapped]
        public string TypeIcon => Type switch
        {
            EventType.DJNight => "🪩",
            EventType.Concert => "🎤",
            EventType.PubQuiz => "📖",
            EventType.Festival => "🎪",
            _ => "🎵"
        };

        [NotMapped]
        public string TypeColor => Type switch
        {
            EventType.DJNight => "#7c3aed",
            EventType.Concert => "#ef4444",
            EventType.PubQuiz => "#f59e0b",
            EventType.Festival => "#f97316",
            _ => "#7c3aed"
        };

        [NotMapped]
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
