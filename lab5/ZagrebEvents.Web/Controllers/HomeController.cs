using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public HomeController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var venues = _db.Venues
                .Include(v => v.Events)
                .ToList();
            return View(venues);
        }
    }
}
