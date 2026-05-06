namespace ZagrebEvents.Web.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfGuests { get; set; }
        public ReservationStatus Status { get; set; }
        public string Note { get; set; }
        public decimal MinimumSpending { get; set; }
        public User User { get; set; }
        public Table Table { get; set; }
        public Event Event { get; set; }

        public bool IsConfirmed => Status == ReservationStatus.Confirmed;

        public string StatusLabel => Status switch
        {
            ReservationStatus.Confirmed => "Potvrđeno",
            ReservationStatus.Pending => "Na čekanju",
            ReservationStatus.Cancelled => "Otkazano",
            _ => "Nepoznato"
        };

        public string StatusColor => Status switch
        {
            ReservationStatus.Confirmed => "#10b981",
            ReservationStatus.Pending => "#f59e0b",
            ReservationStatus.Cancelled => "#ef4444",
            _ => "#94a3b8"
        };
    }
}
