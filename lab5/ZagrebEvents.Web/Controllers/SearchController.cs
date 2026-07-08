using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;
using ZagrebEvents.Web.Models;

namespace ZagrebEvents.Web.Controllers
{
    // GLOBAL SEARCH: jedna tražilica za izbornik/stranice + podatke (lokacije, eventi, korisnici).
    public class SearchController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        public SearchController(ZagrebEventsDbContext db) => _db = db;

        // Statički registar stranica/izbornika (naziv + sinonimi za pretragu)
        private static readonly (string Title, string Url, string Icon, string Keywords)[] Pages =
        {
            ("Karta",             "/",             "🗺️", "karta mapa home pocetna pinovi klubovi"),
            ("Lokacije",          "/lokacije",     "📍", "lokacije venue klubovi barovi kafici mjesta"),
            ("Svi eventi",        "/eventi",       "🎉", "eventi dogadaji party koncerti kvizovi festivali"),
            ("Moj profil",        "/moj-profil",   "👤", "profil moj racun korisnik postavke"),
            ("Rezervacije",       "/rezervacije",  "🗓️", "rezervacije stolovi bookiranje admin"),
            ("Korisnici",         "/korisnici",    "👥", "korisnici useri administracija admin"),
            ("Recenzije",         "/Review/Index", "⭐", "recenzije ocjene komentari review"),
            ("Prijava",           "/prijava",      "🔑", "prijava login ulogiraj se"),
            ("Registracija",      "/registracija", "📝", "registracija novi racun sign up"),
            ("Odjava",            "/odjava",       "🚪", "odjava logout izlaz"),
        };

        [HttpGet]
        [Route("trazi")]
        [Route("[controller]/[action]")]
        public IActionResult Index(string? q = null)
        {
            var vm = new GlobalSearchViewModel { Query = q ?? "" };
            if (string.IsNullOrWhiteSpace(q) || q.Trim().Length < 2)
                return View(vm);

            q = q.Trim();
            bool isAdmin = User.IsInRole("Admin");

            // 1) Stranice / izbornik
            vm.Pages = Pages
                .Where(p => p.Title.Contains(q, StringComparison.OrdinalIgnoreCase)
                         || p.Keywords.Contains(q, StringComparison.OrdinalIgnoreCase))
                .Select(p => new SearchHit { Title = p.Title, Url = p.Url, Icon = p.Icon, Kind = "Stranica" })
                .ToList();

            // 2) Lokacije
            vm.Venues = _db.Venues
                .Where(v => v.DeletedAt == null &&
                       (v.Name.Contains(q) || v.Address.Contains(q) || v.Description.Contains(q)))
                .OrderBy(v => v.Name).Take(10).ToList()
                .Select(v => new SearchHit
                {
                    Title = v.Name,
                    Subtitle = v.Address,
                    Url = $"/lokacija/{v.Id}",
                    Icon = "📍",
                    Kind = v.Type.ToString()
                }).ToList();

            // 3) Eventi (gosti vide samo nadolazeće, admin sve)
            var now = DateTime.Now;
            var evQuery = _db.Events.Include(e => e.Venue)
                .Where(e => e.DeletedAt == null &&
                       (e.Name.Contains(q) || e.Description.Contains(q) ||
                        (e.Venue != null && e.Venue.Name.Contains(q))));
            if (!isAdmin) evQuery = evQuery.Where(e => e.StartTime > now);

            vm.Events = evQuery.OrderBy(e => e.StartTime).Take(10).ToList()
                .Select(e => new SearchHit
                {
                    Title = e.Name,
                    Subtitle = $"{e.Venue?.Name} · {e.StartTime:dd.MM.yyyy HH:mm}",
                    Url = $"/event/{e.Id}",
                    Icon = e.TypeIcon,
                    Kind = e.TypeLabel
                }).ToList();

            // 4) Korisnici — samo admin (privatnost)
            if (isAdmin)
            {
                vm.Users = _db.Users
                    .Where(u => u.DeletedAt == null &&
                           (u.FirstName.Contains(q) || u.LastName.Contains(q) || u.Email.Contains(q)))
                    .OrderBy(u => u.LastName).Take(10).ToList()
                    .Select(u => new SearchHit
                    {
                        Title = u.FullName,
                        Subtitle = u.Email,
                        Url = $"/User/Details/{u.Id}",
                        Icon = "👤",
                        Kind = u.Role.ToString()
                    }).ToList();
            }

            return View(vm);
        }
    }
}
