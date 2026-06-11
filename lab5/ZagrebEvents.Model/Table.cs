using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        public int TableNumber { get; set; }
        public int SeatCount { get; set; }
        public TableZone Zone { get; set; }

        // Pozicija stola na tlocrtu venuea, u postocima (0-100) sirine/visine slike.
        // null = stol se ne prikazuje kao marker na tlocrtu (samo u popisu).
        public double? PosX { get; set; }
        public double? PosY { get; set; }

        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }
        public virtual Venue? Venue { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
