using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Web.Controllers.Api
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationApiController : ControllerBase
    {
        private readonly ZagrebEventsDbContext _db;
        public ReservationApiController(ZagrebEventsDbContext db) => _db = db;

        private static ReservationDto ToDto(Reservation r) => new()
        {
            Id = r.Id,
            CreatedAt = r.CreatedAt,
            NumberOfGuests = r.NumberOfGuests,
            Status = r.Status.ToString(),
            Note = r.Note,
            MinimumSpending = r.MinimumSpending,
            User = r.User == null ? null : new UserSummaryDto { Id = r.User.Id, FullName = r.User.FullName },
            Event = r.Event == null ? null : new EventSummaryDto { Id = r.Event.Id, Name = r.Event.Name },
            TableId = r.TableId,
            TableNumber = r.Table?.TableNumber ?? 0
        };

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> GetAll(string? q = null)
        {
            var query = _db.Reservations
                .Include(r => r.User).Include(r => r.Event).Include(r => r.Table)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(r =>
                    (r.User != null && (r.User.FirstName.Contains(q) || r.User.LastName.Contains(q))) ||
                    (r.Event != null && r.Event.Name.Contains(q)));
            return Ok(query.OrderByDescending(r => r.CreatedAt).ToList().Select(ToDto).ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<ReservationDto> GetById(int id)
        {
            var r = _db.Reservations
                .Include(x => x.User).Include(x => x.Event).Include(x => x.Table)
                .FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();
            return Ok(ToDto(r));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<ReservationDto> Create([FromBody] ReservationCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_db.Events.Any(e => e.Id == dto.EventId)) return BadRequest("Event ne postoji.");
            if (!_db.Tables.Any(t => t.Id == dto.TableId)) return BadRequest("Stol ne postoji.");
            if (!_db.Users.Any(u => u.Id == dto.UserId)) return BadRequest("Korisnik ne postoji.");

            var r = new Reservation
            {
                NumberOfGuests = dto.NumberOfGuests,
                Note = dto.Note,
                Status = (ReservationStatus)dto.Status,
                UserId = dto.UserId,
                EventId = dto.EventId,
                TableId = dto.TableId,
                CreatedAt = DateTime.Now
            };
            _db.Reservations.Add(r);
            _db.SaveChanges();

            var created = _db.Reservations
                .Include(x => x.User).Include(x => x.Event).Include(x => x.Table)
                .First(x => x.Id == r.Id);
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, ToDto(created));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ReservationDto> Update(int id, [FromBody] ReservationCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var r = _db.Reservations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();

            r.NumberOfGuests = dto.NumberOfGuests;
            r.Note = dto.Note;
            r.Status = (ReservationStatus)dto.Status;
            r.TableId = dto.TableId;
            _db.SaveChanges();

            var updated = _db.Reservations
                .Include(x => x.User).Include(x => x.Event).Include(x => x.Table)
                .First(x => x.Id == r.Id);
            return Ok(ToDto(updated));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var r = _db.Reservations.FirstOrDefault(x => x.Id == id);
            if (r == null) return NotFound();
            _db.Reservations.Remove(r);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
