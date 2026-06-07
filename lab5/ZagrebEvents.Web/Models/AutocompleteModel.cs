namespace ZagrebEvents.Web.Models
{
    public class AutocompleteModel
    {
        public string Name { get; set; } = "";
        public int? InitialId { get; set; }
        public string InitialLabel { get; set; } = "";
        public string Endpoint { get; set; } = "";
        public string Label { get; set; } = "";
        public string Placeholder { get; set; } = "Pretraži...";
        public bool Required { get; set; } = false;
        public int MinChars { get; set; } = 1;
    }
}
