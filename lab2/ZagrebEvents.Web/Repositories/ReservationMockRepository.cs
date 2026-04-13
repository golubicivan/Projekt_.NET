using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Repositories
{
    public class ReservationMockRepository
    {
        public List<Reservation> GetAll() => MockData.Reservations;
        public Reservation? GetById(int id) => MockData.Reservations.FirstOrDefault(r => r.Id == id);
        public List<Reservation> GetByUser(int userId) => MockData.Reservations.Where(r => r.User.Id == userId).ToList();
        public List<Reservation> GetByEvent(int eventId) => MockData.Reservations.Where(r => r.Event.Id == eventId).ToList();
    }
}
