using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
        private async Task<string?> SaveIdentityDocumentAsync(IFormFile? document, string field, string label)
        {
            if (document == null || document.Length == 0)
            {
                ModelState.AddModelError(field, $"Priložite sliku {label} osobnog dokumenta.");
                return null;
            }
            if (document.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError(field, $"Slika {label} je prevelika (max 5 MB).");
                return null;
            }
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp", ".heic" };
            var ext = Path.GetExtension(document.FileName).ToLowerInvariant();
            if (!allowed.Contains(ext))
            {
                ModelState.AddModelError(field, "Dozvoljene su samo slike (JPG, PNG, WEBP).");
                return null;
            }

            // App_Data NIJE javno dostupan (izvan wwwroot-a) - slike sluzi iskljucivo
            // admin-only endpoint /dokument/{fileName} ispod.
            var dir = Path.Combine(_env.ContentRootPath, "App_Data", "documents");
            Directory.CreateDirectory(dir);
            var storedName = Guid.NewGuid().ToString("N") + ext;
            var fullPath = Path.Combine(dir, storedName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
                await document.CopyToAsync(stream);

            return $"/dokument/{storedName}";
        }

        // Fizicka putanja spremljenog dokumenta iz URL-a (/dokument/{fileName})
        private string PhysicalDocPath(string documentUrl) =>
            Path.Combine(_env.ContentRootPath, "App_Data", "documents", Path.GetFileName(documentUrl));

        // Slike osobnih dokumenata smije vidjeti SAMO admin (privatnost).
        // Datoteke su u App_Data (nisu web-dostupne), sluzi ih ovaj autorizirani endpoint.
        [Authorize(Roles = "Admin")]
        [Route("dokument/{fileName}")]
        public IActionResult IdentityDocument(string fileName)
        {
            fileName = Path.GetFileName(fileName);   // sprjecava path traversal
            var path = Path.Combine(_env.ContentRootPath, "App_Data", "documents", fileName);
            if (!System.IO.File.Exists(path)) return NotFound();

            var contentType = Path.GetExtension(fileName).ToLowerInvariant() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                ".heic" => "image/heic",
                _ => "application/octet-stream"
            };
            return PhysicalFile(path, contentType);
        }

        // AI provjera obje strane osobne.
        //   Error    = poruka za korisnika (podaci se ne podudaraju) ili null
        //   Verified = true samo ako je AI stvarno potvrdio podudaranje
        // Kad AI nije dostupan (nema kljuca/kredita, format): Error=null, Verified=false
        // -> registracija prolazi, ali identitet nije potvrdjen (admin ga moze potvrditi rucno).
        private async Task<(string? Error, bool Verified)> RunAiIdentityCheckAsync(
            string frontPath, string backPath, string firstName, string lastName, DateTime dateOfBirth, string oib)
        {
            var front = PhysicalDocPath(frontPath);
            var back = PhysicalDocPath(backPath);
            var check = await _ai.CheckIdentityAsync(front, back, firstName, lastName, dateOfBirth, oib);
            if (check == null) return (null, false);     // AI nedostupan -> propusti, ali bez potvrde
            if (check.Valid) return (null, true);

            System.IO.File.Delete(front);
            System.IO.File.Delete(back);
            var detalj = !check.DocumentVisible
                ? "na slikama nije prepoznat čitljiv osobni dokument"
                : !check.NameMatch
                    ? $"ime na dokumentu ({(string.IsNullOrWhiteSpace(check.FoundName) ? "nečitljivo" : check.FoundName)}) ne odgovara unesenom"
                    : !check.DobMatch
                        ? $"datum rođenja na dokumentu ({(string.IsNullOrWhiteSpace(check.FoundDob) ? "nečitljiv" : check.FoundDob)}) ne odgovara unesenom"
                        : $"OIB na dokumentu ({(string.IsNullOrWhiteSpace(check.FoundOib) ? "nečitljiv" : check.FoundOib)}) ne odgovara unesenom";
            return ($"🤖 AI provjera dokumenta nije prošla: {detalj}. {check.Reason}", false);
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
            DateTime dateOfBirth, string? phoneNumber, string oib,
            IFormFile? documentFront, IFormFile? documentBack, bool skipDocument = false)
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

            // Slike obje strane osobnog dokumenta.
            // Korisnik moze odabrati "ne zelim priloziti osobnu" -> registracija prolazi,
            // ali bez potvrdjenog identiteta (ogranicene rezervacije, vidi ReservationPolicy).
            string? frontPath = null, backPath = null;
            bool identityVerified = false;

            if (!skipDocument)
            {
                frontPath = await SaveIdentityDocumentAsync(documentFront, "documentFront", "prednje strane");
                backPath = await SaveIdentityDocumentAsync(documentBack, "documentBack", "stražnje strane");
            }

            if (!ModelState.IsValid)
                return View();

            // AI provjera: ime + datum rodjenja (prednja) i OIB (straznja) moraju odgovarati dokumentu.
            // Tehnicke greske (nema kljuca/kredita, nepodrzan format) ne blokiraju registraciju.
            if (frontPath != null && backPath != null)
            {
                var (aiError, verified) = await RunAiIdentityCheckAsync(frontPath, backPath, firstName, lastName, dateOfBirth, oib);
                if (aiError != null)
                {
                    ModelState.AddModelError("documentFront", aiError);
                    return View();
                }
                identityVerified = verified;
            }

            // 1. Kreiraj Identity nalog
            var appUser = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                OIB = oib,
                IdentityDocumentPath = frontPath,
                IdentityDocumentBackPath = backPath,
                IdentityVerified = identityVerified
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

        // ===================== NAKNADNA POTVRDA IDENTITETA =====================
        // Za korisnike koji su pri registraciji preskocili osobnu (ili im AI tada nije bio dostupan).
        [Authorize]
        [HttpGet]
        [Route("potvrdi-identitet")]
        public async Task<IActionResult> VerifyIdentity()
        {
            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null) return RedirectToAction(nameof(Login));
            ViewBag.Verified = appUser.IdentityVerified;
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("potvrdi-identitet")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyIdentity(IFormFile? documentFront, IFormFile? documentBack)
        {
            var appUser = await _userManager.GetUserAsync(User);
            if (appUser == null) return RedirectToAction(nameof(Login));

            var domainUser = _db.Users.FirstOrDefault(u => u.AppUserId == appUser.Id);
            if (domainUser == null) return NotFound();

            var frontPath = await SaveIdentityDocumentAsync(documentFront, "documentFront", "prednje strane");
            var backPath = await SaveIdentityDocumentAsync(documentBack, "documentBack", "stražnje strane");

            if (!ModelState.IsValid || frontPath == null || backPath == null)
            {
                ViewBag.Verified = appUser.IdentityVerified;
                return View();
            }

            // Provjera ide protiv podataka koji su VEC u profilu (ne moze ih se ovdje mijenjati)
            var (aiError, verified) = await RunAiIdentityCheckAsync(
                frontPath, backPath, domainUser.FirstName, domainUser.LastName, domainUser.DateOfBirth, appUser.OIB);

            if (aiError != null)
            {
                ModelState.AddModelError("documentFront", aiError);
                ViewBag.Verified = false;
                return View();
            }

            appUser.IdentityDocumentPath = frontPath;
            appUser.IdentityDocumentBackPath = backPath;
            appUser.IdentityVerified = verified;
            await _userManager.UpdateAsync(appUser);

            // Osvjezi cookie da nova prava odmah vrijede
            await _signInManager.RefreshSignInAsync(appUser);

            TempData["Success"] = verified
                ? "🤖 Identitet je potvrđen — sada možeš rezervirati stolove i na eventima s dobnom granicom."
                : "Dokument je spremljen, ali AI provjera trenutno nije dostupna. Administrator će ga provjeriti ručno.";
            return RedirectToAction("Details", "User", new { id = domainUser.Id });
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
            string firstName, string lastName, string oib, DateTime dateOfBirth,
            IFormFile? documentFront, IFormFile? documentBack, bool skipDocument = false, string? returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(Login));

            var email = info.Principal.FindFirstValue(System.Security.Claims.ClaimTypes.Email) ?? "";

            if (string.IsNullOrWhiteSpace(oib) || oib.Length != 11 || !oib.All(char.IsDigit))
                ModelState.AddModelError(nameof(oib), "OIB mora imati točno 11 znamenki.");

            if (dateOfBirth == default || dateOfBirth > DateTime.Today || dateOfBirth.Year < 1900)
                ModelState.AddModelError("dateOfBirth", "Unesi valjan datum rođenja.");

            string? frontPath = null, backPath = null;
            bool identityVerified = false;

            if (!skipDocument)
            {
                frontPath = await SaveIdentityDocumentAsync(documentFront, "documentFront", "prednje strane");
                backPath = await SaveIdentityDocumentAsync(documentBack, "documentBack", "stražnje strane");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Email = email;
                ViewBag.Provider = info.LoginProvider;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation");
            }

            // Ista AI provjera dokumenta kao kod obicne registracije
            if (frontPath != null && backPath != null)
            {
                var (aiError, verified) = await RunAiIdentityCheckAsync(frontPath, backPath, firstName, lastName, dateOfBirth, oib);
                if (aiError != null)
                {
                    ModelState.AddModelError("documentFront", aiError);
                    ViewBag.Email = email;
                    ViewBag.Provider = info.LoginProvider;
                    ViewBag.ReturnUrl = returnUrl;
                    return View("ExternalLoginConfirmation");
                }
                identityVerified = verified;
            }

            var appUser = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                OIB = oib,
                IdentityDocumentPath = frontPath,
                IdentityDocumentBackPath = backPath,
                IdentityVerified = identityVerified
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
                    DateOfBirth = dateOfBirth,
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
