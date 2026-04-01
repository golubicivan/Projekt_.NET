namespace ZagrebEvents.Model
{
    public class Table
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int SeatCount { get; set; }
        public TableZone Zone { get; set; }

        // N-1: Stol pripada jednoj lokaciji
        public Venue Venue { get; set; }
    }
}
