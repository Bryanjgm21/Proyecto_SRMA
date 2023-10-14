using Microsoft.AspNetCore.Mvc;

namespace SRMA.Controllers
{
    public class ReservationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminCreate()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminEdit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminReservation()
        {
            return View();
        }
    }
}
