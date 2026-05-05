using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public ReviewController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // CUSTOM ROUTE: /recenzije (i default /Review/Index)
        [Route("recenzije")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            var reviews = _db.Reviews
                .Include(r => r.User)
                .Include(r => r.Event).ThenInclude(e => e!.Venue)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
            return View(reviews);
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

        // GET: /Review/Create?eventId=3  -- SVI PRIJAVLJENI
        [Authorize]
        public IActionResult Create(int? eventId)
        {
            var ev = eventId.HasValue ? _db.Events.Find(eventId.Value) : null;
            ViewBag.Event = ev;
            ViewBag.Events = _db.Events.OrderBy(e => e.Name).ToList();
            return View(new Review { EventId = eventId ?? 0, Rating = 5 });
        }

        // POST: /Review/Create  -- SVI PRIJAVLJENI
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Events = _db.Events.OrderBy(e => e.Name).ToList();
                ViewBag.Event = _db.Events.Find(review.EventId);
                return View(review);
            }

            // Uzmi UserId iz prijavljenog korisnika (claim)
            review.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            review.CreatedAt = DateTime.Now;
            _db.Reviews.Add(review);
            _db.SaveChanges();

            TempData["Success"] = "Hvala na recenziji!";
            return RedirectToAction("Details", "Event", new { id = review.EventId });
        }
    }
}
