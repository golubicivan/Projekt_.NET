namespace ZagrebEvents.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfGuests { get; set; }
        public ReservationStatus Status { get; set; }
        public string Note { get; set; }
        public decimal MinimumSpending { get; set; }

        // N-1: Rezervacija pripada jednom korisniku
        public User User { get; set; }

        // N-1: Rezervacija je za jedan stol
        public Table Table { get; set; }

        // N-1: Rezervacija je za jedan event
        public Event Event { get; set; }

        public bool IsConfirmed => Status == ReservationStatus.Confirmed;
    }
}
