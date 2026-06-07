using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Dtos;

namespace ZagrebEvents.Web.Controllers.Api
{
    // User API je zaštićen (privatnost): samo Admin smije čitati popis/detalje.
    // Create/Update se ne rade kroz ovaj API jer nalozi nastaju kroz Identity registraciju.
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    public class UserApiController : ControllerBase
    {
        private readonly ZagrebEventsDbContext _db;
        public UserApiController(ZagrebEventsDbContext db) => _db = db;

        private static UserDto ToDto(User u) => new()
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            FullName = u.FullName,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Role = u.Role.ToString(),
            Age = u.Age
        };

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll(string? q = null)
        {
            var query = _db.Users.Where(u => u.DeletedAt == null);
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(u => u.FirstName.Contains(q) || u.LastName.Contains(q) || u.Email.Contains(q));
            return Ok(query.OrderBy(u => u.LastName).ToList().Select(ToDto).ToList());
        }

        [HttpGet("{id:int}")]
        public ActionResult<UserDto> GetById(int id)
        {
            var u = _db.Users.FirstOrDefault(x => x.Id == id && x.DeletedAt == null);
            if (u == null) return NotFound();
            return Ok(ToDto(u));
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var u = _db.Users.FirstOrDefault(x => x.Id == id && x.DeletedAt == null);
            if (u == null) return NotFound();
            u.DeletedAt = DateTime.UtcNow;   // soft delete
            _db.SaveChanges();
            return NoContent();
        }
    }
}
