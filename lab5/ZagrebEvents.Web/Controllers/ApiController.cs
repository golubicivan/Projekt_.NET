using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Web.Controllers
{
    // API controller za AJAX pozive (autocomplete, search)
    [Route("Api/[action]")]
    public class ApiController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public ApiController(ZagrebEventsDbContext db) => _db = db;

        // GET: /Api/SearchVenues?q=Park
        [HttpGet]
        public IActionResult SearchVenues(string q)
        {
            q = (q ?? "").Trim();
            var query = _db.Venues
                .Where(v => v.DeletedAt == null);

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(v => v.Name.Contains(q) || v.Address.Contains(q));
            }

            var results = query
                .OrderBy(v => v.Name)
                .Take(10)
                .Select(v => new {
                    id = v.Id,
                    label = v.Name,
                    subtitle = v.Address
                })
                .ToList();

            return Json(results);
        }

        // GET: /Api/SearchUsers?q=ivan
        [HttpGet]
        public IActionResult SearchUsers(string q)
        {
            q = (q ?? "").Trim();
            var query = _db.Users
                .Where(u => u.DeletedAt == null);

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(q) ||
                    u.LastName.Contains(q) ||
                    u.Email.Contains(q));
            }

            var results = query
                .OrderBy(u => u.LastName)
                .Take(10)
                .Select(u => new {
                    id = u.Id,
                    label = u.FirstName + " " + u.LastName,
                    subtitle = u.Email
                })
                .ToList();

            return Json(results);
        }
    }
}
