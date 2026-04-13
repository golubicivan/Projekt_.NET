using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.Web.Repositories;

namespace ZagrebEvents.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ReservationMockRepository _reservationRepo;

        public ReservationController(ReservationMockRepository reservationRepo)
        {
            _reservationRepo = reservationRepo;
        }

        public IActionResult Index()
        {
            var reservations = _reservationRepo.GetAll();
            return View(reservations);
        }

        public IActionResult Details(int id)
        {
            var reservation = _reservationRepo.GetById(id);
            if (reservation == null) return NotFound();
            return View(reservation);
        }
    }
}
