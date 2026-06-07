using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    // Datoteka (slika/dokument) vezana uz konkretan Event.
    // Fizička datoteka je na disku (wwwroot/uploads/events/{eventId}), metapodaci u bazi.
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        public virtual Event? Event { get; set; }

        [Required, MaxLength(260)]
        public string FileName { get; set; } = "";

        [Required, MaxLength(500)]
        public string FilePath { get; set; } = "";

        [MaxLength(120)]
        public string ContentType { get; set; } = "";

        public long FileSize { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
