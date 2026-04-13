using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.Web.Repositories;

namespace ZagrebEvents.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly VenueMockRepository _venueRepo;

        public HomeController(VenueMockRepository venueRepo)
        {
            _venueRepo = venueRepo;
        }

        public IActionResult Index()
        {
            var venues = _venueRepo.GetAll();
            return View(venues);
        }
    }
}
