using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class PriceListItemController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        public PriceListItemController(ZagrebEventsDbContext db) => _db = db;

        // INDEX — samo Admin (cjenik se gleda samo po lokaciji)
        [Authorize(Roles = "Admin")]
        [Route("cjenik")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null)
        {
            var query = _db.PriceListItems.Include(p => p.Venue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(p =>
                    p.ItemName.Contains(q) ||
                    p.Category.Contains(q) ||
                    (p.Venue != null && p.Venue.Name.Contains(q)));
            }
            ViewBag.Query = q;
            return View(query.OrderBy(p => p.Venue!.Name).ThenBy(p => p.Category).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("PriceListItem/SearchPartial")]
        public IActionResult SearchPartial(string? q = null)
        {
            var query = _db.PriceListItems.Include(p => p.Venue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(p =>
                    p.ItemName.Contains(q) ||
                    p.Category.Contains(q) ||
                    (p.Venue != null && p.Venue.Name.Contains(q)));
            }
            return PartialView("_PriceListItemListPartial", query.OrderBy(p => p.Venue!.Name).ThenBy(p => p.Category).ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create(int? venueId = null)
        {
            var model = new PriceListItem();
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PriceListItem item)
        {
            if (!ModelState.IsValid) return View(item);
            _db.PriceListItems.Add(item);
            _db.SaveChanges();
            TempData["Success"] = $"Stavka '{item.ItemName}' dodana u cjenik.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var p = _db.PriceListItems.Find(id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var p = _db.PriceListItems.Find(id);
            if (p == null) return NotFound();
            var ok = await TryUpdateModelAsync(p, "", x => x.ItemName, x => x.Price, x => x.Category, x => x.VenueId);
            if (!ok || !ModelState.IsValid) return View("Edit", p);
            _db.SaveChanges();
            TempData["Success"] = "Stavka cjenika ažurirana.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var p = _db.PriceListItems.Find(id);
            if (p == null) return NotFound();
            _db.PriceListItems.Remove(p);
            _db.SaveChanges();
            TempData["Success"] = "Stavka cjenika obrisana.";
            return RedirectToAction("Index");
        }
    }
}
