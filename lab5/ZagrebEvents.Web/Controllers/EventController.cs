using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Services;

namespace ZagrebEvents.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        private readonly IWebHostEnvironment _env;

        public EventController(ZagrebEventsDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // INDEX — lista evenata (admin vidi sve, ostali samo nadolazeće)
        [Route("eventi")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null)
        {
            var isAdmin = User.IsInRole("Admin");
            var now = DateTime.Now;

            var query = _db.Events
                .Include(e => e.Venue)
                .Include(e => e.Reviews)
                .Where(e => e.DeletedAt == null);

            if (!isAdmin)
            {
                query = query.Where(e => e.StartTime > now);   // samo nadolazeći
            }

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
            var isAdmin = User.IsInRole("Admin");
            var now = DateTime.Now;

            var query = _db.Events
                .Include(e => e.Venue)
                .Include(e => e.Reviews)
                .Where(e => e.DeletedAt == null);

            if (!isAdmin)
            {
                query = query.Where(e => e.StartTime > now);
            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(e =>
                    e.Name.Contains(q) ||
                    (e.Venue != null && e.Venue.Name.Contains(q)));
            }

            var events = query.OrderBy(e => e.StartTime).ToList();
            return PartialView("_EventListPartial", events);
        }

        // DETAILS — guest ne smije vidjeti zavrsene evente
        [Route("event/{id:int}")]
        [Route("[controller]/[action]/{id:int}")]
        public IActionResult Details(int id)
        {
            var ev = _db.Events
                .Include(e => e.Venue).ThenInclude(v => v!.Tables)
                .Include(e => e.Venue).ThenInclude(v => v!.PriceList)
                .Include(e => e.Reservations).ThenInclude(r => r.User)
                .Include(e => e.Reservations).ThenInclude(r => r.Table)
                .Include(e => e.Reviews).ThenInclude(r => r.User)
                .FirstOrDefault(e => e.Id == id && e.DeletedAt == null);

            if (ev == null) return NotFound();

            // Zavrsene evente vidi samo admin
            if (!ev.IsUpcoming && !User.IsInRole("Admin"))
                return NotFound();

            return View(ev);
        }

        // Pomoćna: smije li trenutni korisnik upravljati eventima ovog venuea
        private bool CanManageVenueId(int venueId)
        {
            var ownerId = _db.Venues.Where(v => v.Id == venueId)
                .Select(v => v.OwnerAppUserId).FirstOrDefault();
            return User.CanManageVenue(ownerId);
        }

        // CREATE — GET (Admin ili Owner; može primiti ?venueId)
        [Authorize(Roles = "Admin,Owner")]
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
                    if (!User.CanManageVenue(venue.OwnerAppUserId)) return Forbid();
                    model.VenueId = venue.Id;
                    model.Venue = venue;
                }
            }
            return View(model);
        }

        // CREATE — POST
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event ev)
        {
            if (ev.EndTime <= ev.StartTime)
                ModelState.AddModelError(nameof(Event.EndTime), "Kraj eventa mora biti nakon početka.");
            if (!_db.Venues.Any(v => v.Id == ev.VenueId && v.DeletedAt == null))
                ModelState.AddModelError(nameof(Event.VenueId), "Odabrana lokacija ne postoji.");

            // Owner smije kreirati event samo na svom venueu
            if (!CanManageVenueId(ev.VenueId))
                return Forbid();

            if (!ModelState.IsValid)
                return View(ev);

            _db.Events.Add(ev);
            _db.SaveChanges();
            TempData["Success"] = $"Event '{ev.Name}' uspješno kreiran!";
            return RedirectToAction("Details", new { id = ev.Id });
        }

        // EDIT — GET
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int id)
        {
            var ev = _db.Events
                .Include(e => e.Venue)
                .FirstOrDefault(e => e.Id == id && e.DeletedAt == null);
            if (ev == null) return NotFound();
            if (!CanManageVenueId(ev.VenueId)) return Forbid();
            return View(ev);
        }

        // EDIT — POST
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var ev = _db.Events.FirstOrDefault(e => e.Id == id && e.DeletedAt == null);
            if (ev == null) return NotFound();
            if (!CanManageVenueId(ev.VenueId)) return Forbid();

            var ok = await TryUpdateModelAsync(ev, "",
                e => e.Name, e => e.Description, e => e.StartTime, e => e.EndTime,
                e => e.Type, e => e.EntryPrice, e => e.PosterUrl, e => e.AgeLimit, e => e.VenueId);

            // Ako mijenja venue, mora smjeti upravljati i novim venueom
            if (!CanManageVenueId(ev.VenueId))
                return Forbid();

            if (ev.EndTime <= ev.StartTime)
            {
                ModelState.AddModelError(nameof(Event.EndTime), "Kraj eventa mora biti nakon početka.");
                ok = false;
            }

            if (!ok || !ModelState.IsValid)
                return View("Edit", ev);

            _db.SaveChanges();
            TempData["Success"] = $"Event '{ev.Name}' uspješno ažuriran.";
            return RedirectToAction("Details", new { id = ev.Id });
        }

        // DELETE — POST (soft delete)
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var ev = _db.Events.Find(id);
            if (ev == null) return NotFound();
            if (!CanManageVenueId(ev.VenueId)) return Forbid();

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

            var userId = User.GetDomainUserId() ?? 0;

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

        // ===================== UPLOAD DATOTEKA (Dropzone) =====================

        // POST: /Event/UploadAttachment?eventId=5  (Dropzone šalje multipart form data)
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public async Task<IActionResult> UploadAttachment(int eventId, IFormFile file)
        {
            var ev = _db.Events.FirstOrDefault(e => e.Id == eventId && e.DeletedAt == null);
            if (ev == null) return NotFound();
            if (!CanManageVenueId(ev.VenueId)) return Forbid();
            if (file == null || file.Length == 0) return BadRequest("Datoteka je prazna.");

            // Validacija veličine (max 5 MB) i ekstenzije
            if (file.Length > 5 * 1024 * 1024)
                return BadRequest("Datoteka je prevelika (max 5 MB).");
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".pdf" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
                return BadRequest("Nedozvoljen tip datoteke.");

            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", "events", eventId.ToString());
            Directory.CreateDirectory(uploadsPath);

            var storedName = Guid.NewGuid().ToString("N") + ext;
            var fullPath = Path.Combine(uploadsPath, storedName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                EventId = eventId,
                FileName = file.FileName,
                FilePath = $"/uploads/events/{eventId}/{storedName}",
                ContentType = file.ContentType,
                FileSize = file.Length,
                CreatedAt = DateTime.UtcNow
            };
            _db.Attachments.Add(attachment);
            await _db.SaveChangesAsync();

            return Json(new { success = true, id = attachment.Id });
        }

        // GET: /Event/GetAttachments?eventId=5  (AJAX učitavanje popisa)
        [HttpGet]
        public IActionResult GetAttachments(int eventId)
        {
            var attachments = _db.Attachments
                .Where(a => a.EventId == eventId)
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
            return PartialView("_AttachmentList", attachments);
        }

        // POST: /Event/DeleteAttachment  (AJAX brisanje)
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult DeleteAttachment(int id)
        {
            var attachment = _db.Attachments.Include(a => a.Event).FirstOrDefault(a => a.Id == id);
            if (attachment == null) return NotFound();
            if (attachment.Event != null && !CanManageVenueId(attachment.Event.VenueId)) return Forbid();

            // Obriši fizičku datoteku
            var physicalPath = Path.Combine(_env.WebRootPath, attachment.FilePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (System.IO.File.Exists(physicalPath))
                System.IO.File.Delete(physicalPath);

            _db.Attachments.Remove(attachment);
            _db.SaveChanges();
            return Json(new { success = true });
        }
    }
}
