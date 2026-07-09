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
        private readonly IWebHostEnvironment _env;
        private readonly Services.IAiEventService _ai;

        public AccountController(
            ZagrebEventsDbContext db,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IWebHostEnvironment env,
            Services.IAiEventService ai)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _ai = ai;
        }

        // Sprema priloženu sliku dokumenta na disk i vraća relativnu putanju (ili null uz grešku u ModelState).
        private async Task<string?> SaveIdentityDocumentAsync(IFormFile? document)
        {
            if (document == null || document.Length == 0)
            {
                ModelState.AddModelError("document", "Priložite sliku osobnog dokumenta (potvrda dobi i identiteta).");
                return null;
            }
            if (document.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("document", "Slika je prevelika (max 5 MB).");
                return null;
            }
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp", ".heic" };
            var ext = Path.GetExtension(document.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
            {
                ModelState.AddModelError("document", "Dozvoljene su samo slike (JPG, PNG, WEBP).");
                return null;
            }

            var dir = Path.Combine(_env.WebRootPath, "uploads", "documents");
            Directory.CreateDirectory(dir);
            var storedName = Guid.NewGuid().ToString("N") + ext;
            var fullPath = Path.Combine(dir, storedName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
                await document.CopyToAsync(stream);

            return $"/uploads/documents/{storedName}";
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
            DateTime dateOfBirth, string? phoneNumber, string oib, IFormFile? document)
        {
            // Server-side validacija
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                ModelState.AddModelError("", "Sva obavezna polja moraju biti popunjena.");

            if (password != passwordConfirm)
                ModelState.AddModelError("", "Lozinke se ne podudaraju.");

            if (string.IsNullOrWhiteSpace(oib) || oib.Length != 11 || !oib.All(char.IsDigit))
                ModelState.AddModelError(nameof(oib), "OIB mora imati točno 11 znamenki.");

            if (await _userManager.FindByEmailAsync(email) != null)
                ModelState.AddModelError("", "Korisnik s tim emailom već postoji.");

            if (dateOfBirth == default || dateOfBirth > DateTime.Today || dateOfBirth.Year < 1900)
                ModelState.AddModelError("dateOfBirth", "Unesi valjan datum rođenja.");

            // Slika dokumenta (potvrda dobi i identiteta) - obavezno
            var documentPath = await SaveIdentityDocumentAsync(document);

            if (!ModelState.IsValid)
                return View();

            // AI provjera: ime i datum rodjenja moraju odgovarati podacima na slici dokumenta.
            // Tehnicke greske (nema kljuca/kredita, nepodrzan format) ne blokiraju registraciju.
            if (documentPath != null)
            {
                var physical = Path.Combine(_env.WebRootPath,
                    documentPath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                var check = await _ai.CheckDocumentAsync(physical, firstName, lastName, dateOfBirth);
                if (check != null && !check.Valid)
                {
                    System.IO.File.Delete(physical);
                    var detalj = !check.DocumentVisible
                        ? "na slici nije prepoznat čitljiv osobni dokument"
                        : !check.NameMatch
                            ? $"ime na dokumentu ({(string.IsNullOrWhiteSpace(check.FoundName) ? "nečitljivo" : check.FoundName)}) ne odgovara unesenom"
                            : $"datum rođenja na dokumentu ({(string.IsNullOrWhiteSpace(check.FoundDob) ? "nečitljiv" : check.FoundDob)}) ne odgovara unesenom";
                    ModelState.AddModelError("document", $"🤖 AI provjera dokumenta nije prošla: {detalj}. {check.Reason}");
                    return View();
                }
            }

            // 1. Kreiraj Identity nalog
            var appUser = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                OIB = oib,
                IdentityDocumentPath = documentPath
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
            string firstName, string lastName, string oib, IFormFile? document, string? returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(oib) || oib.Length != 11 || !oib.All(char.IsDigit))
                ModelState.AddModelError(nameof(oib), "OIB mora imati točno 11 znamenki.");

            var documentPath = await SaveIdentityDocumentAsync(document);

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
                IdentityDocumentPath = documentPath
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
