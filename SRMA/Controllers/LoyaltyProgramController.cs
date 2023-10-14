using Microsoft.AspNetCore.Mvc;

namespace SRMA.Controllers
{
    public class LoyaltyProgramController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
