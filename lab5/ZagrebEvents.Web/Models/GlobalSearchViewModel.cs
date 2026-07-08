namespace ZagrebEvents.Web.Models
{
    // Rezultat globalne pretrage (stranice + podaci), grupiran po vrsti.
    public class GlobalSearchViewModel
    {
        public string Query { get; set; } = "";
        public List<SearchHit> Pages { get; set; } = new();
        public List<SearchHit> Venues { get; set; } = new();
        public List<SearchHit> Events { get; set; } = new();
        public List<SearchHit> Users { get; set; } = new();

        public int TotalCount => Pages.Count + Venues.Count + Events.Count + Users.Count;
    }

    public class SearchHit
    {
        public string Title { get; set; } = "";
        public string Subtitle { get; set; } = "";
        public string Url { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Kind { get; set; } = "";
    }
}
