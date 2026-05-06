using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    public class PriceListItem
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string ItemName { get; set; } = "";

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Category { get; set; } = "";

        [ForeignKey(nameof(Venue))]
        public int VenueId { get; set; }
        public virtual Venue? Venue { get; set; }
    }
}
