using Microsoft.AspNetCore.Mvc;
using SRMA.Models;
using System.Diagnostics;
using SRMA.Entities;
using SRMA.Interfaces;

namespace SRMA.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserModel _userModel;

        public LoginController(ILogger<LoginController> logger, IUserModel userModel)
        {
            _logger = logger;
            _userModel = userModel;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Open LogIn View
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        /*
          IdRol Setup 
          1 = Admin
          2 = Employee
          3 = Client
         */

        //Action that makes the LogIn
        [HttpPost]
        public IActionResult LogIn(UserEntity entity)
        {
            var result = _userModel.LogIn(entity);

            if (result != null)
            {
                long userID = result.IdUser;

                HttpContext.Session.SetInt32("IdSession", (int)userID);
                HttpContext.Session.SetString("NameSession", result.userName);
                

                if (result.IdRol == 1)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }

                return RedirectToAction("Index", "User");


            }
            else
            {
                ViewBag.MsjPantalla = "Correo o contraseña incorrectos.";
                return View("LogIn");
            }
        }

        // Open SignUp View
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        //Action that makes the User Registration
        [HttpPost]
        public IActionResult SignUp(UserEntity entity)
        {
            entity.IdRol = 3;
            entity.statusU = true;

            var resultado = _userModel.RegisterUser(entity);

            if (resultado != null)
            {
                return RedirectToAction("LogIn", "Login");
            }
            else
            {
                return RedirectToAction("SignUp", "Login");
            }
        }

        // Open RecoverPassword View
        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }

        //Action that sends the email
        [HttpPost]
        public IActionResult email_Recovery(UserEntity entity)
        {
            var resultado = _userModel.email_Verification(entity);
            if (resultado != null)
            {
                _userModel.Email(entity.email);
                TempData["ContrasenaRecuperada"] = "Su contraseña se envió al correo ingresado.";
                return RedirectToAction("RecoverPassword", "Login");
                // Su contraseña se envió al correo ingresado.
            }
            else
            {
                TempData["ErrorCorreo"] = "No tienes una cuenta, por favor registrate.";
                // No tienes una cuenta, por favor registrate.
                return RedirectToAction("RecoverPassword", "Login");

            }

        }

    }
}