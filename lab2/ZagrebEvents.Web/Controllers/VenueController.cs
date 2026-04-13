using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.Web.Repositories;

namespace ZagrebEvents.Web.Controllers
{
    public class VenueController : Controller
    {
        private readonly VenueMockRepository _venueRepo;

        public VenueController(VenueMockRepository venueRepo)
        {
            _venueRepo = venueRepo;
        }

        public IActionResult Index()
        {
            var venues = _venueRepo.GetAll();
            return View(venues);
        }

        public IActionResult Details(int id)
        {
            var venue = _venueRepo.GetById(id);
            if (venue == null) return NotFound();
            return View(venue);
        }
    }
}
