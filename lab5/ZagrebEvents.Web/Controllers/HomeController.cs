using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZagrebEvents.DAL;

namespace ZagrebEvents.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ZagrebEventsDbContext _db;

        public HomeController(ZagrebEventsDbContext db)
        {
            _db = db;
        }

        // Welcome popup se prikazuje klijentski (sessionStorage u Index.cshtml):
        // svaki novi ulazak u aplikaciju -> popup; povratak na kartu u istoj sesiji -> ne.
        public IActionResult Index()
        {
            var venues = _db.Venues
                .Include(v => v.Events)
                .ToList();
            return View(venues);
        }

        // Prijateljska stranica greske: /greska (500) i /greska/{statusCode} (404, 403...)
        [Route("greska/{code:int?}")]
        public IActionResult Error(int? code = null)
        {
            Response.StatusCode = code ?? 500;
            ViewBag.Code = code ?? 500;
            (ViewBag.Naslov, ViewBag.Poruka) = (code ?? 500) switch
            {
                404 => ("Stranica nije pronađena", "Ova stranica ne postoji ili je uklonjena. Provjeri adresu ili se vrati na kartu."),
                403 => ("Nemaš pristup", "Za ovu stranicu trebaju ti veća prava (npr. Admin ili Owner račun)."),
                _   => ("Nešto je pošlo po zlu", "Dogodila se neočekivana greška. Pokušaj ponovno, a ako se ponavlja — javi administratoru.")
            };
            return View("Error");
        }
    }
}
