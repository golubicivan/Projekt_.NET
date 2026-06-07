using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Web.Controllers.Api
{
    [ApiController]
    [Route("api/tables")]
    public class TableApiController : ControllerBase
    {
        private readonly ZagrebEventsDbContext _db;
        public TableApiController(ZagrebEventsDbContext db) => _db = db;

        private static TableDto ToDto(Table t) => new()
        {
            Id = t.Id,
            TableNumber = t.TableNumber,
            SeatCount = t.SeatCount,
            Zone = t.Zone.ToString(),
            VenueId = t.VenueId,
            VenueName = t.Venue?.Name ?? ""
        };

        [HttpGet]
        public ActionResult<IEnumerable<TableDto>> GetAll(string? q = null)
        {
            var query = _db.Tables.Include(t => t.Venue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(t => t.Venue != null && t.Venue.Name.Contains(q));
            return Ok(query.OrderBy(t => t.VenueId).ThenBy(t => t.TableNumber).ToList().Select(ToDto).ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<TableDto> GetById(int id)
        {
            var t = _db.Tables.Include(x => x.Venue).FirstOrDefault(x => x.Id == id);
            if (t == null) return NotFound();
            return Ok(ToDto(t));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<TableDto> Create([FromBody] TableCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_db.Venues.Any(v => v.Id == dto.VenueId && v.DeletedAt == null))
                return BadRequest("Lokacija ne postoji.");

            var t = new Table
            {
                TableNumber = dto.TableNumber,
                SeatCount = dto.SeatCount,
                Zone = (TableZone)dto.Zone,
                VenueId = dto.VenueId
            };
            _db.Tables.Add(t);
            _db.SaveChanges();
            var created = _db.Tables.Include(x => x.Venue).First(x => x.Id == t.Id);
            return CreatedAtAction(nameof(GetById), new { id = t.Id }, ToDto(created));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<TableDto> Update(int id, [FromBody] TableCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var t = _db.Tables.FirstOrDefault(x => x.Id == id);
            if (t == null) return NotFound();
            t.TableNumber = dto.TableNumber;
            t.SeatCount = dto.SeatCount;
            t.Zone = (TableZone)dto.Zone;
            t.VenueId = dto.VenueId;
            _db.SaveChanges();
            var updated = _db.Tables.Include(x => x.Venue).First(x => x.Id == t.Id);
            return Ok(ToDto(updated));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var t = _db.Tables.FirstOrDefault(x => x.Id == id);
            if (t == null) return NotFound();
            _db.Tables.Remove(t);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
