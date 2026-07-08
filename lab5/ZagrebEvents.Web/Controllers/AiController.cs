using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.DAL;
using ZagrebEvents.Model;
using ZagrebEvents.Web.Services;

namespace ZagrebEvents.Web.Controllers
{
    // AI unos podataka: Admin/Owner opisom na prirodnom jeziku kreira event.
    // Claude parsira upit u strukturirani JSON, mi validiramo i spremamo.
    [Authorize(Roles = "Admin,Owner")]
    public class AiController : Controller
    {
        private readonly ZagrebEventsDbContext _db;
        private readonly IAiEventService _ai;

        public AiController(ZagrebEventsDbContext db, IAiEventService ai)
        {
            _db = db;
            _ai = ai;
        }

        [HttpGet]
        [Route("ai-unos")]
        public IActionResult Index()
        {
            return View(new AiUnosViewModel { KeyMissing = !_ai.IsConfigured });
        }

        [HttpPost]
        [Route("ai-unos")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string prompt)
        {
            var vm = new AiUnosViewModel { Prompt = prompt ?? "", KeyMissing = !_ai.IsConfigured };
            if (string.IsNullOrWhiteSpace(prompt))
            {
                vm.Error = "Upiši opis eventa.";
                return View(vm);
            }

            // Owner smije unositi samo za svoje lokacije - AI vidi samo dopušteni popis
            var venuesQuery = _db.Venues.Where(v => v.DeletedAt == null);
            if (!User.IsInRole("Admin"))
            {
                var appUserId = User.GetAppUserId();
                venuesQuery = venuesQuery.Where(v => v.OwnerAppUserId == appUserId);
            }
            var venues = venuesQuery
                .OrderBy(v => v.Name)
                .Select(v => new { v.Id, v.Name })
                .ToList()
                .Select(v => (v.Id, v.Name))
                .ToList();

            if (venues.Count == 0)
            {
                vm.Error = "Nemaš nijednu lokaciju za koju smiješ kreirati event.";
                return View(vm);
            }

            AiEventDraft draft;
            try
            {
                draft = await _ai.ParseEventAsync(prompt, venues);
            }
            catch (Exception ex)
            {
                vm.Error = $"Greška pri pozivu AI servisa: {ex.Message}";
                return View(vm);
            }

            if (!draft.Ok)
            {
                vm.Error = string.IsNullOrWhiteSpace(draft.Error)
                    ? "AI nije uspio izvući podatke iz upita." : draft.Error;
                return View(vm);
            }

            // Validacija - AI je pomoćnik, server je autoritet
            if (!venues.Any(v => v.Id == draft.VenueId))
            {
                vm.Error = "AI je odabrao lokaciju za koju nemaš prava ili ne postoji. Pokušaj preciznije navesti klub.";
                return View(vm);
            }
            if (!DateTime.TryParse(draft.StartTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out var start) ||
                !DateTime.TryParse(draft.EndTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out var end))
            {
                vm.Error = "AI je vratio neispravan datum. Pokušaj navesti datum jasnije (npr. '15.8. od 22h').";
                return View(vm);
            }
            if (end <= start) end = start.AddHours(4);
            if (string.IsNullOrWhiteSpace(draft.Name))
            {
                vm.Error = "Nedostaje naziv eventa - navedi kako se event zove.";
                return View(vm);
            }

            var ev = new Event
            {
                Name = draft.Name.Length > 150 ? draft.Name[..150] : draft.Name,
                Description = draft.Description.Length > 2000 ? draft.Description[..2000] : draft.Description,
                StartTime = start,
                EndTime = end,
                Type = Enum.IsDefined(typeof(EventType), draft.Type) ? (EventType)draft.Type : EventType.DJNight,
                EntryPrice = Math.Max(0, draft.EntryPrice),
                AgeLimit = Math.Max(0, draft.AgeLimit),
                PosterUrl = "",
                VenueId = draft.VenueId
            };
            _db.Events.Add(ev);
            _db.SaveChanges();

            _db.Entry(ev).Reference(e => e.Venue).Load();
            vm.Created = ev;
            vm.Prompt = "";
            return View(vm);
        }
    }

    public class AiUnosViewModel
    {
        public string Prompt { get; set; } = "";
        public string? Error { get; set; }
        public Event? Created { get; set; }
        public bool KeyMissing { get; set; }
    }
}
