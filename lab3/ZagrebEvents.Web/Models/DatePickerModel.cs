namespace ZagrebEvents.Web.Models
{
    public class DatePickerModel
    {
        public string Name { get; set; } = "";
        public DateTime? Value { get; set; }
        public string Label { get; set; } = "";
        public bool IncludeTime { get; set; } = false;
        public bool Required { get; set; } = false;
    }
}
