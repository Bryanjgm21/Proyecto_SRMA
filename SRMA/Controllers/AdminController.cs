using Microsoft.AspNetCore.Mvc;

namespace SRMA.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
