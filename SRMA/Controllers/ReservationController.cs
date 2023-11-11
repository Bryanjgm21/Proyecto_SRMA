using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SRMA.Entities;
using SRMA.Interfaces;

namespace SRMA.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationModel _reservationModel;

        public ReservationController(IReservationModel reservationModel)
        {
            _reservationModel = reservationModel;
        }

        [HttpGet]
        public IActionResult ViewReservations()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");
            long UserId = (long)IdUser;
            var data = _reservationModel.GetReservationsByUser(UserId);
            return View(data);
        }

        [HttpGet]
        public IActionResult NewReservation()
        {
            return View();
        }



        [HttpPost]
        public IActionResult NewReservation(ReservationEntity entity)
        {
            var resp = _reservationModel.InsertReservationByClient(entity);

            if (resp == 1)
                return RedirectToAction("Index", "Reservation");
            else
            {
                ViewBag.MensajePantalla = "No se pudo registrar su cuenta";
                return View();
            }


        }

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
