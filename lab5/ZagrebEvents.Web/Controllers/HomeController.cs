using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        // Welcome popup se prikaze samo pri PRVOM otvaranju karte nakon pokretanja aplikacije.
        // Restart aplikacije resetira zastavicu (zeljeno ponasanje).
        private static bool _welcomeShown = false;

        public HomeController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.ShowWelcome = !_welcomeShown;
            _welcomeShown = true;

            var venues = _db.Venues
                .Include(v => v.Events)
                .ToList();
            return View(venues);
        }
    }
}
