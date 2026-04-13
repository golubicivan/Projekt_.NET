using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Repositories
{
    public class ReviewMockRepository
    {
        public List<Review> GetAll() => MockData.Reviews;
        public Review? GetById(int id) => MockData.Reviews.FirstOrDefault(r => r.Id == id);
        public List<Review> GetByEvent(int eventId) => MockData.Reviews.Where(r => r.Event.Id == eventId).ToList();
    }
}
