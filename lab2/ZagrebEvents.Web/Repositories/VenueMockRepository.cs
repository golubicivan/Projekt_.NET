using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Repositories
{
    public class VenueMockRepository
    {
        public List<Venue> GetAll() => MockData.Venues;
        public Venue? GetById(int id) => MockData.Venues.FirstOrDefault(v => v.Id == id);
    }
}
