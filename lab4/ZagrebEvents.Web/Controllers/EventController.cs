using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public EventController(ZagrebEventsDbContext db) => _db = db;

        // INDEX — lista evenata (filtrira soft-deleted)
        [Route("eventi")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null)
        {
            var query = _db.Events
                .Include(e => e.Venue)
                .Include(e => e.Reviews)
                .Where(e => e.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(e =>
                    e.Name.Contains(q) ||
                    (e.Venue != null && e.Venue.Name.Contains(q)));
            }

            var events = query.OrderBy(e => e.StartTime).ToList();

            ViewBag.Query = q;
            return View(events);
        }

        // AJAX SEARCH — vraća partial view s listom evenata
        [HttpGet]
        [Route("Event/SearchPartial")]
        public IActionResult SearchPartial(string? q = null)
        {
            var query = _db.Events
                .Include(e => e.Venue)
                .Include(e => e.Reviews)
                .Where(e => e.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(e =>
                    e.Name.Contains(q) ||
                    (e.Venue != null && e.Venue.Name.Contains(q)));
            }

            var events = query.OrderBy(e => e.StartTime).ToList();
            return PartialView("_EventListPartial", events);
        }

        // DETAILS
        [Route("event/{id:int}")]
        [Route("[controller]/[action]/{id:int}")]
        public IActionResult Details(int id)
        {
            var ev = _db.Events
                .Include(e => e.Venue).ThenInclude(v => v!.Tables)
                .Include(e => e.Venue).ThenInclude(v => v!.PriceList)
                .Include(e => e.Reservations)
                .Include(e => e.Reviews).ThenInclude(r => r.User)
                .FirstOrDefault(e => e.Id == id && e.DeletedAt == null);

            if (ev == null) return NotFound();
            return View(ev);
        }

        // CREATE — GET (može primiti ?venueId za predefiniran venue)
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int? venueId = null)
        {
            var model = new Event
            {
                StartTime = DateTime.Today.AddDays(7).AddHours(20),
                EndTime = DateTime.Today.AddDays(7).AddHours(23)
            };
            if (venueId.HasValue)
            {
                var venue = _db.Venues.FirstOrDefault(v => v.Id == venueId.Value && v.DeletedAt == null);
                if (venue != null)
                {
                    model.VenueId = venue.Id;
                    model.Venue = venue;
                }
            }
            return View(model);
        }

        // CREATE — POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event ev)
        {
            // Server-side validacija
            if (ev.EndTime <= ev.StartTime)
            {
                ModelState.AddModelError(nameof(Event.EndTime), "Kraj eventa mora biti nakon početka.");
            }
            if (!_db.Venues.Any(v => v.Id == ev.VenueId && v.DeletedAt == null))
            {
                ModelState.AddModelError(nameof(Event.VenueId), "Odabrana lokacija ne postoji.");
            }

            if (!ModelState.IsValid)
            {
                return View(ev);
            }

            _db.Events.Add(ev);
            _db.SaveChanges();
            TempData["Success"] = $"Event '{ev.Name}' uspješno kreiran!";
            return RedirectToAction("Details", new { id = ev.Id });
        }

        // EDIT — GET
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var ev = _db.Events
                .Include(e => e.Venue)
                .FirstOrDefault(e => e.Id == id && e.DeletedAt == null);
            if (ev == null) return NotFound();
            return View(ev);
        }

        // EDIT — POST (koristi TryUpdateModelAsync)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var ev = _db.Events.FirstOrDefault(e => e.Id == id && e.DeletedAt == null);
            if (ev == null) return NotFound();

            var ok = await TryUpdateModelAsync(ev, "",
                e => e.Name, e => e.Description, e => e.StartTime, e => e.EndTime,
                e => e.Type, e => e.EntryPrice, e => e.PosterUrl, e => e.AgeLimit, e => e.VenueId);

            if (ev.EndTime <= ev.StartTime)
            {
                ModelState.AddModelError(nameof(Event.EndTime), "Kraj eventa mora biti nakon početka.");
                ok = false;
            }

            if (!ok || !ModelState.IsValid)
            {
                return View("Edit", ev);
            }

            _db.SaveChanges();
            TempData["Success"] = $"Event '{ev.Name}' uspješno ažuriran.";
            return RedirectToAction("Details", new { id = ev.Id });
        }

        // DELETE — POST (soft delete)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var ev = _db.Events.Find(id);
            if (ev == null) return NotFound();

            ev.DeletedAt = DateTime.UtcNow;     // SOFT DELETE
            _db.SaveChanges();
            TempData["Success"] = $"Event '{ev.Name}' obrisan.";
            return RedirectToAction("Index");
        }

        // RESERVE
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Reserve(int eventId, int tableId, int guests, string? note)
        {
            var ev = _db.Events.FirstOrDefault(e => e.Id == eventId && e.DeletedAt == null);
            if (ev == null) return NotFound();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var reservation = new Reservation
            {
                EventId = eventId,
                TableId = tableId,
                UserId = userId,
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
