namespace ZagrebEvents.Model
{
    public class PriceListItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; } // "Pice", "Hrana", "Ulaznica"

        // N-1: Stavka pripada jednoj lokaciji
        public Venue Venue { get; set; }
    }
}
