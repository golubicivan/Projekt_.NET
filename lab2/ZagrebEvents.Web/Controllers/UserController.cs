using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.Web.Repositories;

namespace ZagrebEvents.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserMockRepository _userRepo;

        public UserController(UserMockRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            var users = _userRepo.GetAll();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null) return NotFound();
            return View(user);
        }
    }
}
