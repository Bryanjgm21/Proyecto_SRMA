using Microsoft.AspNetCore.Mvc;
using SRMA.Entities;
using SRMA.Interfaces;
using SRMA.Models;

namespace SRMA.Controllers
{
    public class UserController : Controller

    {
        private readonly IUserModel _userModel;

        public UserController(IUserModel userModel)
        {
            _userModel = userModel;
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

            long UserId = (long)IdUser;

            var result = _userModel.ConsultAcc(UserId);

            if (result != null)
            {
                return View(result);
            }

            return NotFound(); // Maneja el caso en el que el usuario no se encuentre
        }

        // Consult the user by their id to update the data
        [HttpPost]
        public IActionResult UpdateUser(UserEntity entity)
        {

            var IdUser = HttpContext.Session.GetInt32("IdSession");

            long UserId = (long)IdUser;

            if (!ModelState.IsValid)
            {
                Thread.Sleep(1500);
                return View("Profile", entity);
            }

            var ver = _userModel.verifUser(entity, UserId);
            if (ver != null)
            {
               ViewBag.ErrorMessage = "El correo ya esta en uso.";
                return View("Profile", entity);
            }
            var ced = _userModel.verCed(entity, UserId);
            if (ced != null)
            {
                ViewBag.ErrorMessage = "La cedula ya esta agregada";
                return View("Profile", entity);
            }

            var resultado = _userModel.UpdateUser(entity, UserId);
            Thread.Sleep(2000);

            if (resultado != null)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Profile", "User");
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
                return RedirectToAction("Profile", "User");
            }
            else
            {
                return RedirectToAction("Profile", "User");
            }
        }
       
    }
}
