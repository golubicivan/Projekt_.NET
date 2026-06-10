using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Services;

namespace ZagrebEvents.Web.Controllers
{
    public class VenueController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public VenueController(ZagrebEventsDbContext db) => _db = db;

        [Route("lokacije")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null, int? type = null)
        {
            ViewBag.Query = q;
            ViewBag.Type = type;
            return View(FilterVenues(q, type));
        }

        [HttpGet]
        [Route("Venue/SearchPartial")]
        public IActionResult SearchPartial(string? q = null, int? type = null)
        {
            return PartialView("_VenueListPartial", FilterVenues(q, type));
        }

        // Zajednička logika filtriranja + sortiranja po tipu
        private List<Venue> FilterVenues(string? q, int? type)
        {
            var query = _db.Venues
                .Include(v => v.Events)
                .Where(v => v.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(v => v.Name.Contains(q) || v.Address.Contains(q));

            if (type.HasValue)
            {
                var vt = (VenueType)type.Value;
                query = query.Where(v => v.Type == vt);
            }

            // Sortiraj po tipu pa po nazivu
            return query.OrderBy(v => v.Type).ThenBy(v => v.Name).ToList();
        }

        [Route("lokacija/{id:int}")]
        [Route("[controller]/[action]/{id:int}")]
        public IActionResult Details(int id)
        {
            var venue = _db.Venues
                .Include(v => v.Events).ThenInclude(e => e.Reviews).ThenInclude(r => r.User)
                .Include(v => v.Tables)
                .Include(v => v.PriceList)
                .FirstOrDefault(v => v.Id == id && v.DeletedAt == null);

            if (venue == null) return NotFound();
            return View(venue);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new Venue());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Venue venue)
        {
            if (!ModelState.IsValid) return View(venue);

            _db.Venues.Add(venue);
            _db.SaveChanges();
            TempData["Success"] = $"Lokacija '{venue.Name}' dodana.";
            return RedirectToAction("Details", new { id = venue.Id });
        }

        // EDIT — Admin ili Owner svog venuea
        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int id)
        {
            var venue = _db.Venues.FirstOrDefault(v => v.Id == id && v.DeletedAt == null);
            if (venue == null) return NotFound();
            if (!User.CanManageVenue(venue.OwnerAppUserId)) return Forbid();
            return View(venue);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var venue = _db.Venues.FirstOrDefault(v => v.Id == id && v.DeletedAt == null);
            if (venue == null) return NotFound();
            if (!User.CanManageVenue(venue.OwnerAppUserId)) return Forbid();

            var ok = await TryUpdateModelAsync(venue, "",
                v => v.Name, v => v.Address, v => v.Latitude, v => v.Longitude,
                v => v.Capacity, v => v.WorkingHours, v => v.ContactPhone,
                v => v.Description, v => v.Type, v => v.ImageUrl, v => v.LogoUrl, v => v.InstagramUrl);

            if (!ok || !ModelState.IsValid) return View("Edit", venue);

            _db.SaveChanges();
            TempData["Success"] = $"Lokacija '{venue.Name}' ažurirana.";
            return RedirectToAction("Details", new { id = venue.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var venue = _db.Venues.Find(id);
            if (venue == null) return NotFound();

            venue.DeletedAt = DateTime.UtcNow;
            _db.SaveChanges();
            TempData["Success"] = $"Lokacija '{venue.Name}' obrisana.";
            return RedirectToAction("Index");
        }
    }
}
