namespace ZagrebEvents.Web.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }

        public string Stars => string.Concat(Enumerable.Repeat("★", Rating)) +
                               string.Concat(Enumerable.Repeat("☆", 5 - Rating));
    }
}
