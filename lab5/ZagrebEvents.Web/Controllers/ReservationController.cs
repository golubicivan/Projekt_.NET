using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Services;

namespace ZagrebEvents.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public ReservationController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // INDEX — samo Admin (rezervacije se gledaju po eventu/lokaciji)
        [Authorize(Roles = "Admin")]
        [Route("rezervacije")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null, int? status = null)
        {
            var query = _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .Include(r => r.Table)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(r =>
                    (r.User != null && (r.User.FirstName.Contains(q) || r.User.LastName.Contains(q))) ||
                    (r.Event != null && r.Event.Name.Contains(q)));
            }

            if (status.HasValue)
            {
                var st = (ReservationStatus)status.Value;
                query = query.Where(r => r.Status == st);
            }

            ViewBag.Query = q;
            ViewBag.Status = status;
            return View(query.OrderByDescending(r => r.CreatedAt).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Reservation/SearchPartial")]
        public IActionResult SearchPartial(string? q = null, int? status = null)
        {
            var query = _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .Include(r => r.Table)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(r =>
                    (r.User != null && (r.User.FirstName.Contains(q) || r.User.LastName.Contains(q))) ||
                    (r.Event != null && r.Event.Name.Contains(q)));
            }
            if (status.HasValue)
            {
                var st = (ReservationStatus)status.Value;
                query = query.Where(r => r.Status == st);
            }

            return PartialView("_ReservationListPartial", query.OrderByDescending(r => r.CreatedAt).ToList());
        }

        // DELETE — hard delete (jer su rezervacije sitan zapis)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var r = _db.Reservations.Find(id);
            if (r == null) return NotFound();
            _db.Reservations.Remove(r);
            _db.SaveChanges();
            TempData["Success"] = $"Rezervacija #{id} obrisana.";
            return RedirectToAction("Index");
        }

        // DETAILS — vlasnik rezervacije ili admin
        [Authorize]
        public IActionResult Details(int id)
        {
            var reservation = _db.Reservations
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .Include(r => r.Table)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();

            var currentUserId = User.GetDomainUserId() ?? 0;
            if (reservation.UserId != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            return View(reservation);
        }

        // Smije li korisnik upravljati rezervacijom (Admin ili Owner venuea tog eventa)
        private bool CanManageReservation(int reservationId)
        {
            var ownerId = _db.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => r.Event!.Venue!.OwnerAppUserId)
                .FirstOrDefault();
            return User.CanManageVenue(ownerId);
        }

        // GET: /Reservation/Edit/5  -- Admin ili Owner venuea
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int id)
        {
            var reservation = _db.Reservations
                .Include(r => r.Event).ThenInclude(e => e!.Venue).ThenInclude(v => v!.Tables)
                .Include(r => r.Table)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null) return NotFound();
            if (!CanManageReservation(id)) return Forbid();
            return View(reservation);
        }

        // POST: /Reservation/Edit/5  -- Admin ili Owner venuea
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, int tableId, int numberOfGuests, string? note, ReservationStatus status)
        {
            var reservation = _db.Reservations.Find(id);
            if (reservation == null) return NotFound();
            if (!CanManageReservation(id)) return Forbid();

            reservation.TableId = tableId;
            reservation.NumberOfGuests = numberOfGuests;
            reservation.Note = note ?? "";
            reservation.Status = status;

            _db.SaveChanges();
            TempData["Success"] = "Rezervacija uspješno ažurirana!";
            return RedirectToAction("Details", new { id });
        }

        // POST: /Reservation/SetStatus/5  -- Admin ili Owner venuea
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult SetStatus(int id, ReservationStatus status, string? returnUrl = null)
        {
            var reservation = _db.Reservations.Find(id);
            if (reservation == null) return NotFound();
            if (!CanManageReservation(id)) return Forbid();

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
