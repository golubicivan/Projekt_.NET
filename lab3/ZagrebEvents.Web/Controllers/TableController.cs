using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class TableController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        public TableController(ZagrebEventsDbContext db) => _db = db;

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

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new Table());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Table table)
        {
            if (!ModelState.IsValid) return View(table);
            _db.Tables.Add(table);
            _db.SaveChanges();
            TempData["Success"] = $"Stol #{table.TableNumber} dodan.";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var t = _db.Tables.Find(id);
            if (t == null) return NotFound();
            return View(t);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var t = _db.Tables.Find(id);
            if (t == null) return NotFound();
            var ok = await TryUpdateModelAsync(t, "", x => x.TableNumber, x => x.SeatCount, x => x.Zone, x => x.VenueId);
            if (!ok || !ModelState.IsValid) return View("Edit", t);
            _db.SaveChanges();
            TempData["Success"] = "Stol ažuriran.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var t = _db.Tables.Find(id);
            if (t == null) return NotFound();
            _db.Tables.Remove(t);
            _db.SaveChanges();
            TempData["Success"] = "Stol obrisan.";
            return RedirectToAction("Index");
        }
    }
}
