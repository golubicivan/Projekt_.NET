using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Services;

namespace ZagrebEvents.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public ReviewController(ZagrebEventsDbContext db) => _db = db;

        [Route("recenzije")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null, int? minRating = null)
        {
            var query = _db.Reviews
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(r =>
                    r.Comment.Contains(q) ||
                    (r.User != null && (r.User.FirstName.Contains(q) || r.User.LastName.Contains(q))) ||
                    (r.Event != null && r.Event.Name.Contains(q)));
            }
            if (minRating.HasValue)
            {
                query = query.Where(r => r.Rating >= minRating.Value);
            }

            ViewBag.Query = q;
            ViewBag.MinRating = minRating;
            return View(query.OrderByDescending(r => r.CreatedAt).ToList());
        }

        [HttpGet]
        [Route("Review/SearchPartial")]
        public IActionResult SearchPartial(string? q = null, int? minRating = null)
        {
            var query = _db.Reviews
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(r =>
                    r.Comment.Contains(q) ||
                    (r.User != null && (r.User.FirstName.Contains(q) || r.User.LastName.Contains(q))) ||
                    (r.Event != null && r.Event.Name.Contains(q)));
            }
            if (minRating.HasValue)
            {
                query = query.Where(r => r.Rating >= minRating.Value);
            }

            return PartialView("_ReviewListPartial", query.OrderByDescending(r => r.CreatedAt).ToList());
        }

        public IActionResult Details(int id)
        {
            var review = _db.Reviews
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .FirstOrDefault(r => r.Id == id);
            if (review == null) return NotFound();
            return View(review);
        }

        [Authorize]
        public IActionResult Create(int? eventId)
        {
            var ev = eventId.HasValue ? _db.Events.Find(eventId.Value) : null;
            ViewBag.Event = ev;
            ViewBag.Events = _db.Events.Where(e => e.DeletedAt == null).OrderBy(e => e.Name).ToList();
            return View(new Review { EventId = eventId ?? 0, Rating = 5 });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Events = _db.Events.Where(e => e.DeletedAt == null).OrderBy(e => e.Name).ToList();
                ViewBag.Event = _db.Events.Find(review.EventId);
                return View(review);
            }

            review.UserId = User.GetDomainUserId() ?? 0;
            review.CreatedAt = DateTime.Now;
            _db.Reviews.Add(review);
            _db.SaveChanges();

            TempData["Success"] = "Hvala na recenziji!";
            return RedirectToAction("Details", "Event", new { id = review.EventId });
        }

        // EDIT — samo autor ili admin
        [Authorize]
        public IActionResult Edit(int id)
        {
            var review = _db.Reviews
                .Include(r => r.Event)
                .FirstOrDefault(r => r.Id == id);
            if (review == null) return NotFound();

            var userId = User.GetDomainUserId() ?? 0;
            if (review.UserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            return View(review);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> EditPost(int id)
        {
            var review = _db.Reviews.FirstOrDefault(r => r.Id == id);
            if (review == null) return NotFound();

            var userId = User.GetDomainUserId() ?? 0;
            if (review.UserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            var ok = await TryUpdateModelAsync(review, "",
                r => r.Rating, r => r.Comment);

            if (!ok || !ModelState.IsValid)
            {
                return View("Edit", review);
            }

            _db.SaveChanges();
            TempData["Success"] = "Recenzija ažurirana.";
            return RedirectToAction("Details", new { id });
        }

        // DELETE — autor ili admin (hard delete - sitan zapis)
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var review = _db.Reviews.Find(id);
            if (review == null) return NotFound();

            var userId = User.GetDomainUserId() ?? 0;
            if (review.UserId != userId && !User.IsInRole("Admin"))
                return Forbid();

            var eventId = review.EventId;
            _db.Reviews.Remove(review);
            _db.SaveChanges();
            TempData["Success"] = "Recenzija obrisana.";
            return RedirectToAction("Details", "Event", new { id = eventId });
        }
    }
}
