using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Repositories
{
    public class EventMockRepository
    {
        public List<Event> GetAll() => MockData.Events;
        public Event? GetById(int id) => MockData.Events.FirstOrDefault(e => e.Id == id);
        public List<Event> GetUpcoming() => MockData.Events.Where(e => e.IsUpcoming).OrderBy(e => e.StartTime).ToList();
        public List<Event> GetByVenue(int venueId) => MockData.Events.Where(e => e.Venue.Id == venueId).ToList();
    }
}
