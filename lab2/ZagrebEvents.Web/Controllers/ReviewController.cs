using Microsoft.AspNetCore.Mvc;
using ZagrebEvents.Web.Repositories;

namespace ZagrebEvents.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewMockRepository _reviewRepo;

        public ReviewController(ReviewMockRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public IActionResult Index()
        {
            var reviews = _reviewRepo.GetAll();
            return View(reviews);
        }

        public IActionResult Details(int id)
        {
            var review = _reviewRepo.GetById(id);
            if (review == null) return NotFound();
            return View(review);
        }
    }
}
