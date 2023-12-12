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
        private readonly IFidelityProModel _fidelityProModel;

        public ReservationController(IReservationModel reservationModel, IFidelityProModel fidelityProModel)
        {
            _reservationModel = reservationModel;
            _fidelityProModel = fidelityProModel;
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
                if (entity.dateReservation == DateTime.MinValue)
                {
                    ViewBag.MsjError = "La fecha y hora de reservación son obligatorias.";
                    return View("NewReservation", entity);
                }
                else
                {
                    return View("NewReservation", entity);
                }
                
            }
            var result= _reservationModel.VerifyReservation(entity, UserId);

            if (result == 1)
            {
                TempData["MensajeError"] = "Error al agregar la reservacion. Ya cuenta con una reserva para esa fecha y hora";
                return RedirectToAction("NewReservation");
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
        public IActionResult AdminReservation()
        {
            var reservations = _reservationModel.ListReservations();
            return View(reservations);
            
        }

        [HttpPost]
        public IActionResult InsertPoints(long IdUser)
        {
            int qty = 10;

            var data = _fidelityProModel.ConsultPoints(IdUser);

            if (data == null)
            {
                ViewBag.MsjError = "Reservación activada";
            }

            var result = _fidelityProModel.InsertPoints(IdUser, qty);

            if (result != null)
            {
                return RedirectToAction("AdminReservation");
            }
            else
            {
                return RedirectToAction("AdminReservation");
            }
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

        [HttpPost]
        public IActionResult EditReser(ReservationEntity entity)
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");
            long UserId = (long)IdUser;

            if (!ModelState.IsValid)
            {
                if (entity.dateReservation == DateTime.MinValue)
                {
                    ViewBag.MsjError = "La fecha y hora de reservación son obligatorias.";
                    return View("AdminEdit", entity);
                }
                else
                {
                    return View("AdminEdit", entity);
                }
               
            }
            var result = _reservationModel.VerifyReservation(entity, UserId);

            if (result == 1)
            {
                TempData["MensajeError"] = "Error al agregar la reservacion. Ya cuenta con una reserva para esa fecha y hora";
                return RedirectToAction("AdminEdit");
            }

            var resultado = _reservationModel.UpdateReservation(entity, UserId);


            if (resultado != null)
            {
                System.Threading.Thread.Sleep(3000);
                return RedirectToAction("AdminEdit", "Reservation");
            }
            else
            {
                return RedirectToAction("AdminEdit", "Reservation");
            }
        }

        [HttpPost]
        public IActionResult DeleteReser(long IdUser)
        {
            var result=_reservationModel.DeleteReser(IdUser);

            if (result != null)
            {
                return RedirectToAction("ViewReservations", new { IdReservation = result.IdReservation });
            }
            else
            {
                return RedirectToAction("ViewReservations");
            }
        }


    }

    


}
