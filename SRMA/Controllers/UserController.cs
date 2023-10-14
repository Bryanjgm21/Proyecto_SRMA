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

            var resultado = _userModel.UpdateUser(entity, UserId);


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
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("LogIn", "Login");
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
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Profile", "User");
            }
        }
       

        // Register user with rol of employee
        [HttpPost]
        public IActionResult RegisterEmployee(UserEntity entity)
        {
            entity.IdRol = 2;
            entity.statusU = true;
            entity.passwordU = "password1";

            var resultado = _userModel.RegisterUser(entity);

            if (resultado != null)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return RedirectToAction("Create", "Employee");
            }
        }
    }
}
