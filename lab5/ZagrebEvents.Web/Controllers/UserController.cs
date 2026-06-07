using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Web.Models;
using ZagrebEvents.Web.Services;

namespace ZagrebEvents.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ZagrebEvents.Model.AppUser> _userManager;

        public UserController(
            ZagrebEventsDbContext db,
            Microsoft.AspNetCore.Identity.UserManager<ZagrebEvents.Model.AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

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
            var currentUserId = User.GetDomainUserId() ?? 0;
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
            var userId = User.GetDomainUserId() ?? 0;
            return RedirectToAction("Details", new { id = userId });
        }

        // EDIT — samo svoje (ili admin za druge)
        [Authorize]
        public IActionResult Edit(int id)
        {
            var currentUserId = User.GetDomainUserId() ?? 0;
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
        public async Task<IActionResult> Edit(int id, UserEditViewModel vm)
        {
            var currentUserId = User.GetDomainUserId() ?? 0;
            if (currentUserId != id && !User.IsInRole("Admin"))
                return Forbid();

            var user = _db.Users.FirstOrDefault(u => u.Id == id && u.DeletedAt == null);
            if (user == null) return NotFound();

            // Vrati read-only polja u vm za prikaz
            vm.FirstName = user.FirstName;
            vm.LastName = user.LastName;
            vm.DateOfBirth = user.DateOfBirth;

            // Identity nalog za promjenu lozinke / emaila
            var appUser = string.IsNullOrEmpty(user.AppUserId)
                ? null : await _userManager.FindByIdAsync(user.AppUserId);

            // Provjera trenutne lozinke kroz Identity
            if (appUser == null || !await _userManager.CheckPasswordAsync(appUser, vm.CurrentPassword))
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

            // Promjena lozinke kroz Identity
            if (!string.IsNullOrEmpty(vm.NewPassword) && appUser != null)
            {
                var pwResult = await _userManager.ChangePasswordAsync(appUser, vm.CurrentPassword, vm.NewPassword);
                if (!pwResult.Succeeded)
                {
                    foreach (var e in pwResult.Errors)
                        ModelState.AddModelError("", e.Description);
                    return View(vm);
                }
            }

            // Mapiraj SAMO dopuštena polja (email + telefon)
            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            if (appUser != null)
            {
                appUser.Email = vm.Email;
                appUser.UserName = vm.Email;
                await _userManager.UpdateAsync(appUser);
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
