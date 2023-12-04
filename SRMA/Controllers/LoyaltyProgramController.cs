using Microsoft.AspNetCore.Mvc;
using SRMA.Entities;
using SRMA.Interfaces;
using SRMA.Models;

namespace SRMA.Controllers
{
    public class LoyaltyProgramController : Controller
    {
        private readonly IFidelityProModel _fidelityProModel;

        public LoyaltyProgramController(IFidelityProModel fidelityProModel)
        {
            _fidelityProModel = fidelityProModel;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var result = _fidelityProModel.ConsultPoints(UserId);

            if (result != null)
            {
                return View(result);
            }

            return View(result);
        }

        [HttpPost]
        public IActionResult RedeemP(string code, int qty)
        {

            var result = _fidelityProModel.RedeemPoints(code, qty);

            if (result != null)
            {
                return View(result);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
