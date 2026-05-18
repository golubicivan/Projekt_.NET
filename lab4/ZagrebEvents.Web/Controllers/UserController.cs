using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public UserController(ZagrebEventsDbContext db) => _db = db;

        // INDEX — samo Admin (privatnost)
        [Authorize(Roles = "Admin")]
        [Route("korisnici")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null)
        {
            var query = _db.Users.Where(u => u.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(q) ||
                    u.LastName.Contains(q) ||
                    u.Email.Contains(q));
            }

            ViewBag.Query = q;
            return View(query.OrderBy(u => u.LastName).ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("User/SearchPartial")]
        public IActionResult SearchPartial(string? q = null)
        {
            var query = _db.Users.Where(u => u.DeletedAt == null);
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(q) ||
                    u.LastName.Contains(q) ||
                    u.Email.Contains(q));
            }
            return PartialView("_UserListPartial", query.OrderBy(u => u.LastName).ToList());
        }

        // DETAILS — vlasnik profila ili admin (privatnost)
        [Authorize]
        public IActionResult Details(int id)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (currentUserId != id && !User.IsInRole("Admin"))
                return Forbid();

            var user = _db.Users
                .Include(u => u.Reservations).ThenInclude(r => r.Event).ThenInclude(e => e!.Venue)
                .Include(u => u.Reservations).ThenInclude(r => r.Table)
                .Include(u => u.Reviews).ThenInclude(r => r.Event)
                .Include(u => u.FavoriteVenues)
                .FirstOrDefault(u => u.Id == id && u.DeletedAt == null);

            if (user == null) return NotFound();
            return View(user);
        }

        [Route("moj-profil")]
        [Authorize]
        public IActionResult MyProfile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return RedirectToAction("Details", new { id = userId });
        }

        // EDIT — samo svoje (ili admin za druge)
        [Authorize]
        public IActionResult Edit(int id)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (currentUserId != id && !User.IsInRole("Admin"))
                return Forbid();

            var user = _db.Users.FirstOrDefault(u => u.Id == id && u.DeletedAt == null);
            if (user == null) return NotFound();

            var vm = new UserEditViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserEditViewModel vm)
        {
            var currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (currentUserId != id && !User.IsInRole("Admin"))
                return Forbid();

            var user = _db.Users.FirstOrDefault(u => u.Id == id && u.DeletedAt == null);
            if (user == null) return NotFound();

            // Vrati read-only polja u vm za prikaz
            vm.FirstName = user.FirstName;
            vm.LastName = user.LastName;
            vm.DateOfBirth = user.DateOfBirth;

            // Server-side validacija lozinke
            if (user.Password != vm.CurrentPassword)
            {
                ModelState.AddModelError(nameof(vm.CurrentPassword), "Pogrešna trenutna lozinka.");
            }
            if (!string.IsNullOrEmpty(vm.NewPassword) && vm.NewPassword != vm.NewPasswordConfirm)
            {
                ModelState.AddModelError(nameof(vm.NewPasswordConfirm), "Nove lozinke se ne podudaraju.");
            }
            if (vm.Email != user.Email && _db.Users.Any(u => u.Id != id && u.Email == vm.Email))
            {
                ModelState.AddModelError(nameof(vm.Email), "Email već koristi drugi korisnik.");
            }

            if (!ModelState.IsValid) return View(vm);

            // Mapiraj SAMO dopuštena polja
            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            if (!string.IsNullOrEmpty(vm.NewPassword))
            {
                user.Password = vm.NewPassword;
            }

            _db.SaveChanges();
            TempData["Success"] = "Podaci uspješno ažurirani.";
            return RedirectToAction("Details", new { id });
        }

        // DELETE — soft delete, samo admin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null) return NotFound();

            user.DeletedAt = DateTime.UtcNow;
            _db.SaveChanges();
            TempData["Success"] = $"Korisnik '{user.FullName}' obrisan.";
            return RedirectToAction("Index");
        }
    }
}
