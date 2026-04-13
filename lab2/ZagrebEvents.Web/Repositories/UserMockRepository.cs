using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Repositories
{
    public class UserMockRepository
    {
        public List<User> GetAll() => MockData.Users;
        public User? GetById(int id) => MockData.Users.FirstOrDefault(u => u.Id == id);
    }
}
