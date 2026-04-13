namespace ZagrebEvents.Web.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int SeatCount { get; set; }
        public TableZone Zone { get; set; }
        public Venue Venue { get; set; }
    }
}
