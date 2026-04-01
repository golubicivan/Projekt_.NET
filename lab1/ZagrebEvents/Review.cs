namespace ZagrebEvents.Model
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; } // 1-5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        // N-1: Recenzija pripada jednom korisniku
        public User User { get; set; }

        // N-1: Recenzija pripada jednom eventu
        public Event Event { get; set; }
    }
}
