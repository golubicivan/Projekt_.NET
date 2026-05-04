using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public UserController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // CUSTOM ROUTE: /korisnici (i default /User/Index)
        [Route("korisnici")]
        public IActionResult Index()
        {
            var users = _db.Users.OrderBy(u => u.LastName).ToList();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _db.Users
                .Include(u => u.Reservations).ThenInclude(r => r.Event).ThenInclude(e => e!.Venue)
                .Include(u => u.Reservations).ThenInclude(r => r.Table)
                .Include(u => u.Reviews).ThenInclude(r => r.Event)
                .Include(u => u.FavoriteVenues)
                .FirstOrDefault(u => u.Id == id);

            if (user == null) return NotFound();
            return View(user);
        }

        // CUSTOM ROUTE: /moj-profil (alias za Details s ID-jem 1 - "ulogiran" korisnik)
        [Route("moj-profil")]
        public IActionResult MyProfile()
        {
            return RedirectToAction("Details", new { id = 1 });
        }
    }
}
