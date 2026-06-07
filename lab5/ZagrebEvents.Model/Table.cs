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

        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }
        public virtual Venue? Venue { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
