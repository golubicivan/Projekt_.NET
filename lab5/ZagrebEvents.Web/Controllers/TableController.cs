using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Services;

namespace ZagrebEvents.Web.Controllers
{
    public class TableController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        public TableController(ZagrebEventsDbContext db) => _db = db;

        private bool CanManageVenueId(int venueId)
        {
            var ownerId = _db.Venues.Where(v => v.Id == venueId)
                .Select(v => v.OwnerAppUserId).FirstOrDefault();
            return User.CanManageVenue(ownerId);
        }

        // INDEX — samo Admin (stolovi se gledaju samo po lokaciji)
        [Authorize(Roles = "Admin")]
        [Route("stolovi")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null)
        {
            var query = _db.Tables.Include(t => t.Venue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(t => t.Venue != null && t.Venue.Name.Contains(q));
            }
            ViewBag.Query = q;
            return View(query.OrderBy(t => t.Venue!.Name).ThenBy(t => t.TableNumber).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("Table/SearchPartial")]
        public IActionResult SearchPartial(string? q = null)
        {
            var query = _db.Tables.Include(t => t.Venue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(t => t.Venue != null && t.Venue.Name.Contains(q));
            }
            return PartialView("_TableListPartial", query.OrderBy(t => t.Venue!.Name).ThenBy(t => t.TableNumber).ToList());
        }

        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Create(int? venueId = null)
        {
            var model = new Table { Zone = TableZone.Regular, SeatCount = 4 };
            if (venueId.HasValue)
            {
                var venue = _db.Venues.FirstOrDefault(v => v.Id == venueId.Value && v.DeletedAt == null);
                if (venue != null)
                {
                    if (!User.CanManageVenue(venue.OwnerAppUserId)) return Forbid();
                    model.VenueId = venue.Id;
                    model.Venue = venue;
                    var lastNum = _db.Tables.Where(t => t.VenueId == venue.Id)
                                            .Select(t => (int?)t.TableNumber).Max();
                    model.TableNumber = (lastNum ?? 0) + 1;
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Table table)
        {
            if (!CanManageVenueId(table.VenueId)) return Forbid();
            if (!ModelState.IsValid) return View(table);
            _db.Tables.Add(table);
            _db.SaveChanges();
            TempData["Success"] = $"Stol #{table.TableNumber} dodan.";
            return RedirectToAction("Details", "Venue", new { id = table.VenueId });
        }

        [Authorize(Roles = "Admin,Owner")]
        public IActionResult Edit(int id)
        {
            var t = _db.Tables.Include(x => x.Venue).FirstOrDefault(x => x.Id == id);
            if (t == null) return NotFound();
            if (!CanManageVenueId(t.VenueId)) return Forbid();
            return View(t);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var t = _db.Tables.Find(id);
            if (t == null) return NotFound();
            if (!CanManageVenueId(t.VenueId)) return Forbid();
            var ok = await TryUpdateModelAsync(t, "", x => x.TableNumber, x => x.SeatCount, x => x.Zone, x => x.VenueId, x => x.PosX, x => x.PosY);
            if (!CanManageVenueId(t.VenueId)) return Forbid();
            if (!ok || !ModelState.IsValid) return View("Edit", t);
            _db.SaveChanges();
            TempData["Success"] = "Stol ažuriran.";
            return RedirectToAction("Details", "Venue", new { id = t.VenueId });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var t = _db.Tables.Find(id);
            if (t == null) return NotFound();
            if (!CanManageVenueId(t.VenueId)) return Forbid();
            var venueId = t.VenueId;
            _db.Tables.Remove(t);
            _db.SaveChanges();
            TempData["Success"] = "Stol obrisan.";
            return RedirectToAction("Details", "Venue", new { id = venueId });
        }
    }
}
