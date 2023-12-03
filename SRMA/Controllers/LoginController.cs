using Microsoft.AspNetCore.Mvc;
using SRMA.Models;
using System.Diagnostics;
using SRMA.Entities;
using SRMA.Interfaces;
using DocumentFormat.OpenXml.Spreadsheet;

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

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn", "Login");
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
                    return RedirectToAction("AdminDashboard", "Admin", new { cameFromLogin = true });
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

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, simplemente regresa a la vista sin intentar registrar el usuario.
                return View("SignUp", entity);
            }

            entity.IdRol = 3;
            entity.statusU = true;

            var ver = _userModel.verifUser(entity,0);
            if (ver != null)
            {
                ViewBag.ErrorMessage = "El correo ya esta en uso.";
                return View("SignUp", entity);
            }

            var ced = _userModel.verCed(entity,0);
            if (ced != null)
            {
                ViewBag.ErrorMessage = "La cedula ya esta agregada";
                return View("SignUp", entity);
            }

            if (entity.passwordU == entity.confirmPassword)
            {
                var resultado = _userModel.RegisterUser(entity);

                if (resultado != null)
                {
                    // Almacenar el mensaje de éxito en ViewBag
                    ViewBag.MensajeExito = "Registro exitoso.";

                    // Agregar un indicador de éxito al modelo para que lo pueda comprobar la vista
                    ModelState.AddModelError("RegistroExitoso", "Registro exitoso");

                    // Devolver la vista sin redirección
                    return View("SignUp", entity);
                }
                else
                {
                    // Manejar error al registrar el usuario
                    ViewBag.MensajeError = "Error al registrar el usuario.";
                }
            }
            else
            {
                // Manejar error de contraseña no coincidente
                ModelState.AddModelError("ConfirmPassword", "La contraseña y la confirmación de contraseña no coinciden.");
            }

            // Devolver la vista con el modelo y mensajes
            return View(entity);
        }




        // Open RecoverPassword View
        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecoverPassword(UserEntity entity)
        {
            var resp = _userModel.RecoverAccount(entity);

            if (resp == 1)
            {
                TempData["ContrasenaRecuperada"] = "Email de recuperación enviado exitosamente";
                return RedirectToAction("RecoverPassword", "Login");
            }
                
            else
            {
                TempData["ErrorCorreo"] = "No tienes una cuenta, por favor registrate.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ChangePassword(string q)
        {
            UserEntity entity = new UserEntity();
            entity.IdUserEncrypt = q;
            return View(entity);
        }

        [HttpPost]
        public IActionResult ChangePassword(UserEntity entity)
        {
            var resp = _userModel.ChangeAccPassword(entity);

            if (resp == 1)

                return RedirectToAction("LogIn", "Login");
            else
            {
                ViewBag.MensajePantalla = "No se pudo actualizar su contraseña";
                return View();
            }
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