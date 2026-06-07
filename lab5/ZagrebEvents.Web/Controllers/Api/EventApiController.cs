using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Web.Controllers.Api
{
    [ApiController]
    [Route("api/events")]
    public class EventApiController : ControllerBase
    {
        private readonly ZagrebEventsDbContext _db;
        public EventApiController(ZagrebEventsDbContext db) => _db = db;

        // ===== Mapiranje entitet -> DTO =====
        private static EventDto ToDto(Event e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            StartTime = e.StartTime,
            EndTime = e.EndTime,
            Type = e.Type.ToString(),
            EntryPrice = e.EntryPrice,
            PosterUrl = e.PosterUrl,
            AgeLimit = e.AgeLimit,
            AverageRating = e.Reviews != null && e.Reviews.Any() ? e.Reviews.Average(r => r.Rating) : 0,
            VenueId = e.VenueId,
            Venue = e.Venue == null ? null : new VenueSummaryDto
            {
                Id = e.Venue.Id,
                Name = e.Venue.Name,
                Address = e.Venue.Address
            }
        };

        // GET /api/events?q=...
        [HttpGet]
        public ActionResult<IEnumerable<EventDto>> GetAll(string? q = null)
        {
            var query = _db.Events
                .Include(e => e.Venue)
                .Include(e => e.Reviews)
                .Where(e => e.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(e => e.Name.Contains(q) || (e.Venue != null && e.Venue.Name.Contains(q)));

            var list = query.OrderBy(e => e.StartTime).ToList().Select(ToDto).ToList();
            return Ok(list);
        }

        // GET /api/events/5
        [HttpGet("{id:int}")]
        public ActionResult<EventDto> GetById(int id)
        {
            var e = _db.Events
                .Include(x => x.Venue)
                .Include(x => x.Reviews)
                .FirstOrDefault(x => x.Id == id && x.DeletedAt == null);

            if (e == null) return NotFound();
            return Ok(ToDto(e));
        }

        // POST /api/events
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<EventDto> Create([FromBody] EventCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (dto.EndTime <= dto.StartTime)
                return BadRequest("Kraj eventa mora biti nakon početka.");
            if (!_db.Venues.Any(v => v.Id == dto.VenueId && v.DeletedAt == null))
                return BadRequest("Odabrana lokacija ne postoji.");

            var e = new Event
            {
                Name = dto.Name,
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Type = (EventType)dto.Type,
                EntryPrice = dto.EntryPrice,
                PosterUrl = dto.PosterUrl,
                AgeLimit = dto.AgeLimit,
                VenueId = dto.VenueId
            };
            _db.Events.Add(e);
            _db.SaveChanges();

            var created = _db.Events.Include(x => x.Venue).First(x => x.Id == e.Id);
            return CreatedAtAction(nameof(GetById), new { id = e.Id }, ToDto(created));
        }

        // PUT /api/events/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<EventDto> Update(int id, [FromBody] EventCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var e = _db.Events.FirstOrDefault(x => x.Id == id && x.DeletedAt == null);
            if (e == null) return NotFound();
            if (dto.EndTime <= dto.StartTime)
                return BadRequest("Kraj eventa mora biti nakon početka.");

            e.Name = dto.Name;
            e.Description = dto.Description;
            e.StartTime = dto.StartTime;
            e.EndTime = dto.EndTime;
            e.Type = (EventType)dto.Type;
            e.EntryPrice = dto.EntryPrice;
            e.PosterUrl = dto.PosterUrl;
            e.AgeLimit = dto.AgeLimit;
            e.VenueId = dto.VenueId;
            _db.SaveChanges();

            var updated = _db.Events.Include(x => x.Venue).First(x => x.Id == e.Id);
            return Ok(ToDto(updated));
        }

        // DELETE /api/events/5 (soft delete)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var e = _db.Events.FirstOrDefault(x => x.Id == id && x.DeletedAt == null);
            if (e == null) return NotFound();
            e.DeletedAt = DateTime.UtcNow;
            _db.SaveChanges();
            return NoContent();
        }
    }
}
