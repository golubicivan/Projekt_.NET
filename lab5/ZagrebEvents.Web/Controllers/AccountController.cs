using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;

namespace ZagrebEvents.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            ZagrebEventsDbContext db,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // ===================== LOGIN =====================
        [HttpGet]
        [Route("prijava")]
        [Route("[controller]/[action]")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("prijava")]
        [Route("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Pogrešan email ili lozinka.");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!, password, isPersistent: true, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Pogrešan email ili lozinka.");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        // ===================== REGISTER =====================
        [HttpGet]
        [Route("registracija")]
        [Route("[controller]/[action]")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("registracija")]
        [Route("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            string firstName, string lastName, string email, string password, string passwordConfirm,
            DateTime dateOfBirth, string? phoneNumber, string oib, string jmbg)
        {
            // Server-side validacija
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                ModelState.AddModelError("", "Sva obavezna polja moraju biti popunjena.");

            if (password != passwordConfirm)
                ModelState.AddModelError("", "Lozinke se ne podudaraju.");

            if (string.IsNullOrWhiteSpace(oib) || oib.Length != 11 || !oib.All(char.IsDigit))
                ModelState.AddModelError(nameof(oib), "OIB mora imati točno 11 znamenki.");

            if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13 || !jmbg.All(char.IsDigit))
                ModelState.AddModelError(nameof(jmbg), "JMBG mora imati točno 13 znamenki.");

            if (await _userManager.FindByEmailAsync(email) != null)
                ModelState.AddModelError("", "Korisnik s tim emailom već postoji.");

            if (!ModelState.IsValid)
                return View();

            // 1. Kreiraj Identity nalog
            var appUser = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                OIB = oib,
                JMBG = jmbg
            };
            var result = await _userManager.CreateAsync(appUser, password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError("", e.Description);
                return View();
            }

            await _userManager.AddToRoleAsync(appUser, "Guest");

            // 2. Kreiraj domenski profil i poveži ga
            var domainUser = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber ?? "",
                DateOfBirth = dateOfBirth,
                Role = UserRole.Guest,
                RegisteredAt = DateTime.Now,
                AppUserId = appUser.Id
            };
            _db.Users.Add(domainUser);
            await _db.SaveChangesAsync();

            // 3. Prijavi korisnika
            await _signInManager.SignInAsync(appUser, isPersistent: true);

            TempData["Success"] = $"Dobrodošao/la, {firstName}!";
            return RedirectToAction("Index", "Home");
        }

        // ===================== LOGOUT =====================
        [Route("odjava")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();

        // ===================== GOOGLE EXTERNAL LOGIN =====================
        [HttpPost]
        [Route("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                TempData["Error"] = $"Greška vanjskog providera: {remoteError}";
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            // Pokušaj prijave ako korisnik već postoji s tim vanjskim loginom
            var signInResult = await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }

            // Prvi put: traži OIB/JMBG da dovrši registraciju
            var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email) ?? "";
            var name = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Name) ?? "";
            ViewBag.Email = email;
            ViewBag.Name = name;
            ViewBag.Provider = info.LoginProvider;
            ViewBag.ReturnUrl = returnUrl;
            return View("ExternalLoginConfirmation");
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(
            string firstName, string lastName, string oib, string jmbg, string? returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(oib) || oib.Length != 11 || !oib.All(char.IsDigit))
                ModelState.AddModelError(nameof(oib), "OIB mora imati točno 11 znamenki.");
            if (string.IsNullOrWhiteSpace(jmbg) || jmbg.Length != 13 || !jmbg.All(char.IsDigit))
                ModelState.AddModelError(nameof(jmbg), "JMBG mora imati točno 13 znamenki.");

            if (!ModelState.IsValid)
            {
                ViewBag.Email = email;
                ViewBag.Provider = info.LoginProvider;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation");
            }

            var appUser = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                OIB = oib,
                JMBG = jmbg
            };
            var createResult = await _userManager.CreateAsync(appUser);
            if (createResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "Guest");
                await _userManager.AddLoginAsync(appUser, info);

                var domainUser = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Role = UserRole.Guest,
                    RegisteredAt = DateTime.Now,
                    AppUserId = appUser.Id
                };
                _db.Users.Add(domainUser);
                await _db.SaveChangesAsync();

                await _signInManager.SignInAsync(appUser, isPersistent: true);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }

            foreach (var e in createResult.Errors)
                ModelState.AddModelError("", e.Description);
            ViewBag.Email = email;
            ViewBag.Provider = info.LoginProvider;
            return View("ExternalLoginConfirmation");
        }
    }
}
