namespace ZagrebEvents.Model
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

        // 1-N: Lokacija ima vise evenata
        public List<Event> Events { get; set; }

        // 1-N: Lokacija ima vise stolova
        public List<Table> Tables { get; set; }

        // 1-N: Lokacija ima cjenik
        public List<PriceListItem> PriceList { get; set; }

        public Venue()
        {
            Events = new List<Event>();
            Tables = new List<Table>();
            PriceList = new List<PriceListItem>();
        }

        public int AvailableTablesCount(Event forEvent)
        {
            var reservedTableIds = forEvent.Reservations
                .Where(r => r.Status != ReservationStatus.Cancelled)
                .Select(r => r.Table.Id)
                .ToList();

            return Tables.Count(t => !reservedTableIds.Contains(t.Id));
        }
    }
}
