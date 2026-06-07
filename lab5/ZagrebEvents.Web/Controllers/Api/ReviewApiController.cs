using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Web.Controllers.Api
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewApiController : ControllerBase
    {
        private readonly ZagrebEventsDbContext _db;
        public ReviewApiController(ZagrebEventsDbContext db) => _db = db;

        private static ReviewDto ToDto(Review r) => new()
        {
            Id = r.Id,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt,
            User = r.User == null ? null : new UserSummaryDto { Id = r.User.Id, FullName = r.User.FullName },
            Event = r.Event == null ? null : new EventSummaryDto { Id = r.Event.Id, Name = r.Event.Name }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ReviewDto>> GetAll(string? q = null, int? minRating = null)
        {
            var query = _db.Reviews.Include(r => r.User).Include(r => r.Event).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(r => r.Comment.Contains(q) ||
                    (r.User != null && (r.User.FirstName.Contains(q) || r.User.LastName.Contains(q))) ||
                    (r.Event != null && r.Event.Name.Contains(q)));
            if (minRating.HasValue)
                query = query.Where(r => r.Rating >= minRating.Value);
            return Ok(query.OrderByDescending(r => r.CreatedAt).ToList().Select(ToDto).ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<ReviewDto> GetById(int id)
        {
            var r = _db.Reviews.Include(x => x.User).Include(x => x.Event).FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();
            return Ok(ToDto(r));
        }

        [HttpPost]
        [Authorize]
        public ActionResult<ReviewDto> Create([FromBody] ReviewCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_db.Events.Any(e => e.Id == dto.EventId)) return BadRequest("Event ne postoji.");
            if (!_db.Users.Any(u => u.Id == dto.UserId)) return BadRequest("Korisnik ne postoji.");

            var r = new Review
            {
                Rating = dto.Rating,
                Comment = dto.Comment,
                UserId = dto.UserId,
                EventId = dto.EventId,
                CreatedAt = DateTime.Now
            };
            _db.Reviews.Add(r);
            _db.SaveChanges();

            var created = _db.Reviews.Include(x => x.User).Include(x => x.Event).First(x => x.Id == r.Id);
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, ToDto(created));
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public ActionResult<ReviewDto> Update(int id, [FromBody] ReviewCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var r = _db.Reviews.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();

            r.Rating = dto.Rating;
            r.Comment = dto.Comment;
            _db.SaveChanges();

            var updated = _db.Reviews.Include(x => x.User).Include(x => x.Event).First(x => x.Id == r.Id);
            return Ok(ToDto(updated));
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var r = _db.Reviews.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();
            _db.Reviews.Remove(r);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
