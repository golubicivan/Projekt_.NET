using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public int NumberOfGuests { get; set; }
        public ReservationStatus Status { get; set; }

        [MaxLength(500)]
        public string Note { get; set; } = "";

        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumSpending { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey(nameof(Table))]
        public int TableId { get; set; }
        public virtual Table? Table { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }

        [NotMapped]
        public bool IsConfirmed => Status == ReservationStatus.Confirmed;

        [NotMapped]
        public string StatusLabel => Status switch
        {
            ReservationStatus.Confirmed => "Potvrđeno",
            ReservationStatus.Pending => "Na čekanju",
            ReservationStatus.Cancelled => "Otkazano",
            _ => "Nepoznato"
        };

        [NotMapped]
        public string StatusColor => Status switch
        {
            ReservationStatus.Confirmed => "#10b981",
            ReservationStatus.Pending => "#f59e0b",
            ReservationStatus.Cancelled => "#ef4444",
            _ => "#94a3b8"
        };
    }
}
