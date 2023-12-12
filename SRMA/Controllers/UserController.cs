using Microsoft.AspNetCore.Mvc;
using SRMA.Entities;
using SRMA.Interfaces;
using SRMA.Models;

namespace SRMA.Controllers
{
    public class UserController : Controller

    {
        private readonly IUserModel _userModel;
        private readonly ILogBookModel _logBookModel;

        public UserController(IUserModel userModel, ILogBookModel logBookModel)
        {
            _userModel = userModel;
            _logBookModel = logBookModel;
        }

        // Open the Home Page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Consult the user by their id to read the data
        [HttpGet]
        public IActionResult Profile()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");
            long q = (long)IdUser;
            try             
            {

                var result = _userModel.ConsultAcc(q);

                if (result != null)
                {
                    return View(result);
                }

                return NotFound(); // Maneja el caso en el que el usuario no se encuentre
            }
            catch (Exception ex)
            {
                var logbook = new LogBookEntity();
                logbook.origin = "UpdateUser";
                logbook.message = ex.Message;
                logbook.log_type = "ERROR";

                _logBookModel.RegisterLogBook(logbook, q);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // Consult the user by their id to update the data
        [HttpPost]
        public IActionResult UpdateUser(UserEntity entity)
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");
            long q = (long)IdUser;
            try
            {
                ModelState.Remove("ptoDays");
                ModelState.Remove("scheduleE");
                ModelState.Remove("job");
                ModelState.Remove("salary");

                if (!ModelState.IsValid)
                {
                    return View("Profile", entity);
                }

                var ver = _userModel.verifUser(entity, q);
                if (ver != null)
                {
                    ViewBag.ErrorMessage = "El correo ya esta en uso.";
                    return View("Profile", entity);
                }
                var ced = _userModel.verCed(entity, q);
                if (ced != null)
                {
                    ViewBag.ErrorMessage = "La cedula ya esta agregada";
                    return View("Profile", entity);
                }

                var resultado = _userModel.UpdateUser(entity, q);

                if (resultado != null)
                {
                    TempData["RegistroExitoso"] = "El usuario se modifico correctamente.";
                    return RedirectToAction("Profile");
                }
                else
                {
                    TempData["MensajeError"] = "Error al modificar el usuario. Por favor, inténtelo de nuevo.";
                }
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                var logbook = new LogBookEntity();
                logbook.origin = "UpdateUser";
                logbook.message = ex.Message;
                logbook.log_type = "ERROR";

                _logBookModel.RegisterLogBook(logbook, q);
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        // Consult the user by their id to delete the user from the table
        [HttpPost]
        public IActionResult DeleteAcc()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var result = _userModel.DeleteAcc(UserId);

            if (result != null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        // Method to Register a user in the loyalty program
        [HttpPost]
        public IActionResult RegisterProgram()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var result = _userModel.RegUserProg(UserId);

            if (result != null)
            {
                TempData["Registro"] = "Se registro correctamente.";
                return RedirectToAction("Profile");
            }
            else
            {
                TempData["Error"] = "Error al registrar. Por favor, inténtelo de nuevo.";
            }
            return RedirectToAction("Profile");
        }

        [HttpGet]
       public IActionResult CodeUser()
        {
            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            var result = _userModel.ConsultAcc(UserId);

            if (result != null)
            {
                return View(result);
            }

            return NotFound(); 
        }
    }
}
