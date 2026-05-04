using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Web.Controllers
{
    public class VenueController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public VenueController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // CUSTOM ROUTE: /lokacije (i default /Venue/Index)
        [Route("lokacije")]
        public IActionResult Index()
        {
            var venues = _db.Venues
                .Include(v => v.Events)
                .ToList();
            return View(venues);
        }

        // CUSTOM ROUTE: /lokacija/{id} (i default /Venue/Details/{id})
        [Route("lokacija/{id:int}")]
        public IActionResult Details(int id)
        {
            var venue = _db.Venues
                .Include(v => v.Events).ThenInclude(e => e.Reviews)
                .Include(v => v.Tables)
                .Include(v => v.PriceList)
                .FirstOrDefault(v => v.Id == id);

            if (venue == null) return NotFound();
            return View(venue);
        }
    }
}
