using System.ComponentModel.DataAnnotations;

namespace ZagrebEvents.Web.Dtos
{
    // ===================== Pomoćni (nested) DTO-ovi =====================
    public class VenueSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
    }

    public class UserSummaryDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
    }

    public class EventSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    // ===================== Event =====================
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Type { get; set; } = "";
        public decimal EntryPrice { get; set; }
        public string PosterUrl { get; set; } = "";
        public int AgeLimit { get; set; }
        public double AverageRating { get; set; }
        public int VenueId { get; set; }
        public VenueSummaryDto? Venue { get; set; }   // ugniježđeni DTO
    }

    public class EventCreateDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = "";
        [MaxLength(2000)]
        public string Description { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Type { get; set; }
        [Range(0, 100000)]
        public decimal EntryPrice { get; set; }
        [MaxLength(500)]
        public string PosterUrl { get; set; } = "";
        [Range(0, 99)]
        public int AgeLimit { get; set; }
        [Required]
        public int VenueId { get; set; }
    }

    // ===================== Venue =====================
    public class VenueDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Capacity { get; set; }
        public string WorkingHours { get; set; } = "";
        public string ContactPhone { get; set; } = "";
        public string Description { get; set; } = "";
        public string Type { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public int EventCount { get; set; }
    }

    public class VenueCreateDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(200)]
        public string Address { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Range(0, 1000000)]
        public int Capacity { get; set; }
        [MaxLength(50)]
        public string WorkingHours { get; set; } = "";
        [MaxLength(30)]
        public string ContactPhone { get; set; } = "";
        [MaxLength(2000)]
        public string Description { get; set; } = "";
        public int Type { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; } = "";
    }

    // ===================== Reservation =====================
    public class ReservationDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; } = "";
        public string Note { get; set; } = "";
        public decimal MinimumSpending { get; set; }
        public UserSummaryDto? User { get; set; }
        public EventSummaryDto? Event { get; set; }
        public int TableId { get; set; }
        public int TableNumber { get; set; }
    }

    public class ReservationCreateDto
    {
        [Range(1, 50)]
        public int NumberOfGuests { get; set; }
        [MaxLength(500)]
        public string Note { get; set; } = "";
        public int Status { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public int TableId { get; set; }
    }

    // ===================== Review =====================
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public UserSummaryDto? User { get; set; }
        public EventSummaryDto? Event { get; set; }
    }

    public class ReviewCreateDto
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        [Required, MaxLength(1000)]
        public string Comment { get; set; } = "";
        [Required]
        public int UserId { get; set; }
        [Required]
        public int EventId { get; set; }
    }

    // ===================== Table =====================
    public class TableDto
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int SeatCount { get; set; }
        public string Zone { get; set; } = "";
        public int VenueId { get; set; }
        public string VenueName { get; set; } = "";
    }

    public class TableCreateDto
    {
        [Range(1, 10000)]
        public int TableNumber { get; set; }
        [Range(1, 100)]
        public int SeatCount { get; set; }
        public int Zone { get; set; }
        [Required]
        public int VenueId { get; set; }
    }

    // ===================== PriceListItem =====================
    public class PriceListItemDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
        public int VenueId { get; set; }
        public string VenueName { get; set; } = "";
    }

    public class PriceListItemCreateDto
    {
        [Required, MaxLength(100)]
        public string ItemName { get; set; } = "";
        [Range(0, 100000)]
        public decimal Price { get; set; }
        [MaxLength(50)]
        public string Category { get; set; } = "";
        [Required]
        public int VenueId { get; set; }
    }

    // ===================== User =====================
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Role { get; set; } = "";
        public int Age { get; set; }
    }
}
