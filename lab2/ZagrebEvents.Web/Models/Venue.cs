namespace ZagrebEvents.Web.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Capacity { get; set; }
        public string WorkingHours { get; set; }
        public string ContactPhone { get; set; }
        public string Description { get; set; }
        public VenueType Type { get; set; }
        public string ImageUrl { get; set; }
        public List<Event> Events { get; set; } = new();
        public List<Table> Tables { get; set; } = new();
        public List<PriceListItem> PriceList { get; set; } = new();

        public int AvailableTablesCount(int eventId)
        {
            var reservedTableIds = Events
                .FirstOrDefault(e => e.Id == eventId)?.Reservations
                .Where(r => r.Status != ReservationStatus.Cancelled)
                .Select(r => r.Table?.Id)
                .ToList() ?? new List<int?>();
            return Tables.Count(t => !reservedTableIds.Contains(t.Id));
        }
    }
}
