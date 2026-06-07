using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Web.Controllers.Api
{
    [ApiController]
    [Route("api/venues")]
    public class VenueApiController : ControllerBase
    {
        private readonly ZagrebEventsDbContext _db;
        public VenueApiController(ZagrebEventsDbContext db) => _db = db;

        private static VenueDto ToDto(Venue v) => new()
        {
            Id = v.Id,
            Name = v.Name,
            Address = v.Address,
            Latitude = v.Latitude,
            Longitude = v.Longitude,
            Capacity = v.Capacity,
            WorkingHours = v.WorkingHours,
            ContactPhone = v.ContactPhone,
            Description = v.Description,
            Type = v.Type.ToString(),
            ImageUrl = v.ImageUrl,
            EventCount = v.Events?.Count(e => e.DeletedAt == null) ?? 0
        };

        [HttpGet]
        public ActionResult<IEnumerable<VenueDto>> GetAll(string? q = null)
        {
            var query = _db.Venues.Include(v => v.Events).Where(v => v.DeletedAt == null);
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(v => v.Name.Contains(q) || v.Address.Contains(q));
            return Ok(query.OrderBy(v => v.Name).ToList().Select(ToDto).ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<VenueDto> GetById(int id)
        {
            var v = _db.Venues.Include(x => x.Events).FirstOrDefault(x => x.Id == id && x.DeletedAt == null);
            if (v == null) return NotFound();
            return Ok(ToDto(v));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<VenueDto> Create([FromBody] VenueCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var v = new Venue
            {
                Name = dto.Name,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Capacity = dto.Capacity,
                WorkingHours = dto.WorkingHours,
                ContactPhone = dto.ContactPhone,
                Description = dto.Description,
                Type = (VenueType)dto.Type,
                ImageUrl = dto.ImageUrl
            };
            _db.Venues.Add(v);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = v.Id }, ToDto(v));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<VenueDto> Update(int id, [FromBody] VenueCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var v = _db.Venues.FirstOrDefault(x => x.Id == id && x.DeletedAt == null);
            if (v == null) return NotFound();

            v.Name = dto.Name;
            v.Address = dto.Address;
            v.Latitude = dto.Latitude;
            v.Longitude = dto.Longitude;
            v.Capacity = dto.Capacity;
            v.WorkingHours = dto.WorkingHours;
            v.ContactPhone = dto.ContactPhone;
            v.Description = dto.Description;
            v.Type = (VenueType)dto.Type;
            v.ImageUrl = dto.ImageUrl;
            _db.SaveChanges();
            return Ok(ToDto(v));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var v = _db.Venues.FirstOrDefault(x => x.Id == id && x.DeletedAt == null);
            if (v == null) return NotFound();
            v.DeletedAt = DateTime.UtcNow;
            _db.SaveChanges();
            return NoContent();
        }
    }
}
