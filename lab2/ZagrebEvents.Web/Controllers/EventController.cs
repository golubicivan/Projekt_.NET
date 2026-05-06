using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.Web.Models;
using ZagrebEvents.Web.Repositories;

namespace ZagrebEvents.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly EventMockRepository _eventRepo;
        private readonly ReservationMockRepository _reservationRepo;

        public EventController(EventMockRepository eventRepo, ReservationMockRepository reservationRepo)
        {
            _eventRepo = eventRepo;
            _reservationRepo = reservationRepo;
        }

        public IActionResult Index()
        {
            var events = _eventRepo.GetAll();
            return View(events);
        }

        public IActionResult Details(int id)
        {
            var ev = _eventRepo.GetById(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        public IActionResult Reserve(int eventId, int tableId, int guests, string note)
        {
            // Mock rezervacija - u pravoj aplikaciji bi se spremila u bazu
            TempData["ReservationSuccess"] = $"Rezervacija za {guests} gostiju uspješno poslana! Čekajte potvrdu.";
            return RedirectToAction("Details", new { id = eventId });
        }
    }
}
