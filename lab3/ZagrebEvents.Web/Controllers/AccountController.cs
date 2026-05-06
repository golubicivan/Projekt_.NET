using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public AccountController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // GET: /Account/Login
        [HttpGet]
        [Route("prijava")]
        [Route("[controller]/[action]")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [Route("prijava")]
        [Route("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || user.Password != password)
            {
                ModelState.AddModelError("", "Pogrešan email ili lozinka.");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            await SignInUser(user);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        [HttpGet]
        [Route("registracija")]
        [Route("[controller]/[action]")]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [Route("registracija")]
        [Route("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string firstName, string lastName, string email, string password, string passwordConfirm, DateTime dateOfBirth, string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Sva obavezna polja moraju biti popunjena.");
                return View();
            }

            if (password.Length < 6)
            {
                ModelState.AddModelError("", "Lozinka mora imati barem 6 znakova.");
                return View();
            }

            if (password != passwordConfirm)
            {
                ModelState.AddModelError("", "Lozinke se ne podudaraju.");
                return View();
            }

            if (_db.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("", "Korisnik s tim emailom već postoji.");
                return View();
            }

            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber ?? "",
                DateOfBirth = dateOfBirth,
                Role = UserRole.Guest,        // Novi korisnici uvijek Guest
                RegisteredAt = DateTime.Now
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            await SignInUser(user);

            TempData["Success"] = $"Dobrodošao/la, {user.FirstName}!";
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        [Route("odjava")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = true });
        }
    }
}
