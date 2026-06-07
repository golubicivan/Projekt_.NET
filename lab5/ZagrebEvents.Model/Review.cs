using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required, MaxLength(1000)]
        public string Comment { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }

        [NotMapped]
        public string Stars => string.Concat(Enumerable.Repeat("★", Rating)) +
                               string.Concat(Enumerable.Repeat("☆", 5 - Rating));
    }
}
