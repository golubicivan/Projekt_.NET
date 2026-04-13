namespace ZagrebEvents.Web.Models
{
    public class PriceListItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public Venue Venue { get; set; }
    }
}
