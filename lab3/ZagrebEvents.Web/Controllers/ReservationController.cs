using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public ReservationController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // CUSTOM ROUTE: /rezervacije (i default /Reservation/Index)
        [Route("rezervacije")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            var reservations = _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .Include(r => r.Table)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
            return View(reservations);
        }

        public IActionResult Details(int id)
        {
            var reservation = _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .Include(r => r.Table)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();
            return View(reservation);
        }

        // GET: /Reservation/Edit/5  -- SAMO ADMIN
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var reservation = _db.Reservations
                .Include(r => r.Event).ThenInclude(e => e!.Venue).ThenInclude(v => v!.Tables)
                .Include(r => r.Table)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();
            return View(reservation);
        }

        // POST: /Reservation/Edit/5  -- SAMO ADMIN
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, int tableId, int numberOfGuests, string? note, ReservationStatus status)
        {
            var reservation = _db.Reservations.Find(id);
            if (reservation == null) return NotFound();

            reservation.TableId = tableId;
            reservation.NumberOfGuests = numberOfGuests;
            reservation.Note = note ?? "";
            reservation.Status = status;

            _db.SaveChanges();
            TempData["Success"] = "Rezervacija uspješno ažurirana!";
            return RedirectToAction("Details", new { id });
        }

        // POST: /Reservation/SetStatus/5  -- BRZA AKCIJA ZA ADMINA
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult SetStatus(int id, ReservationStatus status, string? returnUrl = null)
        {
            var reservation = _db.Reservations.Find(id);
            if (reservation == null) return NotFound();

            reservation.Status = status;
            _db.SaveChanges();

            var label = status switch
            {
                ReservationStatus.Confirmed => "potvrđena",
                ReservationStatus.Cancelled => "otkazana",
                ReservationStatus.Pending => "vraćena na čekanje",
                _ => "ažurirana"
            };
            TempData["Success"] = $"Rezervacija #{id} {label}.";

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }
    }
}
