using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Web.Controllers.Api
{
    [ApiController]
    [Route("api/pricelistitems")]
    public class PriceListItemApiController : ControllerBase
    {
        private readonly ZagrebEventsDbContext _db;
        public PriceListItemApiController(ZagrebEventsDbContext db) => _db = db;

        private static PriceListItemDto ToDto(PriceListItem p) => new()
        {
            Id = p.Id,
            ItemName = p.ItemName,
            Price = p.Price,
            Category = p.Category,
            VenueId = p.VenueId,
            VenueName = p.Venue?.Name ?? ""
        };

        [HttpGet]
        public ActionResult<IEnumerable<PriceListItemDto>> GetAll(string? q = null)
        {
            var query = _db.PriceListItems.Include(p => p.Venue).AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(p => p.ItemName.Contains(q) || p.Category.Contains(q) ||
                    (p.Venue != null && p.Venue.Name.Contains(q)));
            return Ok(query.OrderBy(p => p.VenueId).ThenBy(p => p.Category).ToList().Select(ToDto).ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<PriceListItemDto> GetById(int id)
        {
            var p = _db.PriceListItems.Include(x => x.Venue).FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            return Ok(ToDto(p));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<PriceListItemDto> Create([FromBody] PriceListItemCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_db.Venues.Any(v => v.Id == dto.VenueId && v.DeletedAt == null))
                return BadRequest("Lokacija ne postoji.");

            var p = new PriceListItem
            {
                ItemName = dto.ItemName,
                Price = dto.Price,
                Category = dto.Category,
                VenueId = dto.VenueId
            };
            _db.PriceListItems.Add(p);
            _db.SaveChanges();
            var created = _db.PriceListItems.Include(x => x.Venue).First(x => x.Id == p.Id);
            return CreatedAtAction(nameof(GetById), new { id = p.Id }, ToDto(created));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<PriceListItemDto> Update(int id, [FromBody] PriceListItemCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var p = _db.PriceListItems.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            p.ItemName = dto.ItemName;
            p.Price = dto.Price;
            p.Category = dto.Category;
            p.VenueId = dto.VenueId;
            _db.SaveChanges();
            var updated = _db.PriceListItems.Include(x => x.Venue).First(x => x.Id == p.Id);
            return Ok(ToDto(updated));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var p = _db.PriceListItems.FirstOrDefault(x => x.Id == id);
            if (p == null) return NotFound();
            _db.PriceListItems.Remove(p);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
