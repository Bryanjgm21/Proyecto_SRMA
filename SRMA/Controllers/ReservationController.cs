using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SRMA.Entities;
using SRMA.Interfaces;
using SRMA.Models;

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
            var IdUser = HttpContext.Session.GetInt32("IdSession");
            long UserId = (long)IdUser;


            if (!ModelState.IsValid)
            {
                ViewBag.MsjError = "La fecha y hora de reservación son obligatorias.";

                return View("NewReservation", entity);
            }

            var resp = _reservationModel.InsertReservationByClient(entity, UserId);

            if (resp == 1)
            {
                TempData["RegistroExitoso"] = "La reservacion se agrego correctamente.";
                return RedirectToAction("NewReservation");
            }
            else
            {

                TempData["MensajeError"] = "Error al agregar la reservacion. Por favor, inténtelo de nuevo.";
            }
            return RedirectToAction("NewReservation");
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
        public IActionResult AdminEdit(long q)
        {
            var result = _reservationModel.GetReservationsById(q);

            if (result != null)
            {
                return View(result);
            }
            return NotFound();
        }

        public IActionResult EditReser(ReservationEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return View("AdminEdit", entity);
            }

            var resultado = _reservationModel.UpdateReservation(entity, entity.IdReservation);


            if (resultado != null)
            {
                System.Threading.Thread.Sleep(3000);
                return RedirectToAction("AdminReservation", "Reservation");
            }
            else
            {
                return RedirectToAction("AdminEdit", "Reservation");
            }
        }


        [HttpGet]
        public IActionResult AdminReservation()
        {
            var reservations = _reservationModel.ListReservations();
            return View(reservations);
            
        }


    }
}
