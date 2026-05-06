namespace ZagrebEvents.Web.Models
{
    public enum EventType { DJNight, Concert, PubQuiz, Festival }
    public enum VenueType { Club, Bar, Cafe, OpenAir }
    public enum ReservationStatus { Pending, Confirmed, Cancelled }
    public enum TableZone { Regular, VIP }
    public enum UserRole { Guest, Owner, Admin }
}
