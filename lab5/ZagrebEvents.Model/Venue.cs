using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZagrebEvents.Model
{
    public class Venue
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string Address { get; set; } = "";

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Capacity { get; set; }

        [MaxLength(50)]
        public string WorkingHours { get; set; } = "";

        [MaxLength(30)]
        public string ContactPhone { get; set; } = "";

        [MaxLength(2000)]
        public string Description { get; set; } = "";

        public VenueType Type { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; } = "";

        // Logo venuea (prikazuje se u krugu na karti). Ako prazno, koriste se inicijali.
        [MaxLength(500)]
        public string LogoUrl { get; set; } = "";

        // Soft delete
        public DateTime? DeletedAt { get; set; }

        // Vlasnik venuea (Owner rola). Owner smije uređivati samo svoje venue.
        // Veže se na AppUser.Id (Identity). null = nema dodijeljenog vlasnika (samo admin upravlja).
        [MaxLength(450)]
        public string? OwnerAppUserId { get; set; }

        // 1-N relacije
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
        public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
        public virtual ICollection<PriceListItem> PriceList { get; set; } = new List<PriceListItem>();

        // N-N: korisnici koji imaju ovaj venue u favorites
        public virtual ICollection<User> FavoritedByUsers { get; set; } = new List<User>();

        // Inicijali venuea za prikaz u krugu na karti kad nema logotipa
        [NotMapped]
        public string Initials
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name)) return "?";
                var words = Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 1) return words[0].Substring(0, Math.Min(2, words[0].Length)).ToUpper();
                return (words[0][0].ToString() + words[1][0]).ToUpper();
            }
        }

        public int AvailableTablesCount(int eventId)
        {
            var ev = Events.FirstOrDefault(e => e.Id == eventId);
            if (ev == null) return Tables.Count;
            var reservedTableIds = ev.Reservations
                .Where(r => r.Status != ReservationStatus.Cancelled)
                .Select(r => r.TableId)
                .ToList();
            return Tables.Count(t => !reservedTableIds.Contains(t.Id));
        }
    }
}
