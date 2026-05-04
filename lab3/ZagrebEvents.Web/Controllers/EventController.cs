using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public EventController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // CUSTOM ROUTE: /eventi (i default /Event/Index)
        [Route("eventi")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            var events = _db.Events
                .Include(e => e.Venue)
                .Include(e => e.Reviews)
                .OrderBy(e => e.StartTime)
                .ToList();
            return View(events);
        }

        // CUSTOM ROUTE: /event/{id} (i default /Event/Details/{id})
        [Route("event/{id:int}")]
        [Route("[controller]/[action]/{id:int}")]
        public IActionResult Details(int id)
        {
            var ev = _db.Events
                .Include(e => e.Venue).ThenInclude(v => v!.Tables)
                .Include(e => e.Venue).ThenInclude(v => v!.PriceList)
                .Include(e => e.Reservations)
                .Include(e => e.Reviews).ThenInclude(r => r.User)
                .FirstOrDefault(e => e.Id == id);

            if (ev == null) return NotFound();
            return View(ev);
        }

        // GET: /Event/Create
        public IActionResult Create()
        {
            ViewBag.Venues = _db.Venues.OrderBy(v => v.Name).ToList();
            return View(new Event { StartTime = DateTime.Today.AddDays(7).AddHours(20), EndTime = DateTime.Today.AddDays(7).AddHours(23) });
        }

        // POST: /Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event ev)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Venues = _db.Venues.OrderBy(v => v.Name).ToList();
                return View(ev);
            }

            _db.Events.Add(ev);
            _db.SaveChanges();
            TempData["Success"] = $"Event '{ev.Name}' uspješno kreiran!";
            return RedirectToAction("Details", new { id = ev.Id });
        }

        // POST: /Event/Reserve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserve(int eventId, int tableId, int guests, string? note)
        {
            var ev = _db.Events.Find(eventId);
            if (ev == null) return NotFound();

            var reservation = new Reservation
            {
                EventId = eventId,
                TableId = tableId,
                UserId = 1, // Hardcoded - u pravoj aplikaciji uzima se iz prijavljenog korisnika
                NumberOfGuests = guests,
                Note = note ?? "",
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.Now,
                MinimumSpending = 0
            };

            _db.Reservations.Add(reservation);
            _db.SaveChanges();

            TempData["ReservationSuccess"] = $"Rezervacija za {guests} gostiju uspješno poslana! Čekajte potvrdu.";
            return RedirectToAction("Details", new { id = eventId });
        }
    }
}
