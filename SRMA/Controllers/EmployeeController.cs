using Microsoft.AspNetCore.Mvc;
using SRMA.Entities;
using SRMA.Interfaces;
using static Dapper.SqlMapper;

namespace SRMA.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUserModel _userModel;


        public EmployeeController(IUserModel userModel)
        {
            _userModel = userModel;
        }
        //List of Users by Rol

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userModel.ListUsers(2);
            return View(users);
        }
                
        [HttpGet]
        public IActionResult EmployeeInfo(long IdUser)
        {
           
            var result = _userModel.ConsultAcc(IdUser);

            if (result != null)
            {
                return View(result);
            }

            return NotFound(); // Maneja el caso en el que el usuario no se encuentre
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Register user with rol of employee
        [HttpPost]
        public IActionResult RegisterEmployee(UserEntity entity)
        {
            entity.IdRol = 2;
            entity.statusU = true;
            entity.passwordU = "Password123.@";

            ModelState.Remove("passwordU");

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, simplemente regresa a la vista sin intentar registrar el usuario.
                return View("Create", entity);
            }

            var ver = _userModel.verifUser(entity,0);
            if (ver != null)
            {
                ViewBag.ErrorMessage = "El correo del empleado ya existe";
                return View("Create", entity);
            }

            var ced = _userModel.verCed(entity,0);
            if (ced != null)
            {
                ViewBag.ErrorMessage = "La cedula del empleado ya existe";
                return View("Create", entity);
            }

            var resultado = _userModel.RegisterUser(entity);

            if (resultado != null)
            {
                TempData["RegistroExitoso"] = "Se registro correctamente.";

                // Devolver la vista sin redirección
                return RedirectToAction("Create");
            }
            else
            {
                // Manejar error al registrar el usuario
                TempData["MensajeError"] = "Error al registrar el usuario.";
            }

            // Devolver la vista con el modelo y mensajes
            return RedirectToAction("Create");
        }



        [HttpPost]
        public IActionResult Edit(UserEntity entity)
        {
            entity.passwordU = "Password123.@";

            ModelState.Remove("passwordU");

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, simplemente regresa a la vista sin intentar actualizar el usuario.
                return View("EmployeeInfo", entity);
            }

            var ver = _userModel.verifUser(entity, IdUser);
            if (ver != null)
            {
                ViewBag.ErrorMessage = "El correo del empleado ya existe";
                return View("EmployeeInfo", entity);
            }

            var ced = _userModel.verCed(entity, IdUser);
            if (ced != null)
            {
                ViewBag.ErrorMessage = "La cedula del empleado ya existe";
                return View("EmployeeInfo", entity);
            }

            var resultado = _userModel.UpdateUser(entity, entity.IdUser);

            if (resultado != null)
            {
                // Almacenar el mensaje de éxito en ViewBag
                ViewBag.MensajeExito = "Actualizacion exitosa.";

                // Agregar un indicador de éxito al modelo para que lo pueda comprobar la vista
                ModelState.AddModelError("RegistroExitoso", "Actualizacion exitosa");

                // Devolver la vista sin redirección
                return View("EmployeeInfo", entity);
            }
            else
            {
                // Manejar error al actualizar el usuario
                ViewBag.MensajeError = "Error al actualizar el usuario.";
            }

            // Devolver la vista con el modelo y mensajes
            return View("EmployeeInfo", entity);
        }


        [HttpPost]
        public IActionResult DeleteAcc(long IdUser)
        {
           
            
            var result = _userModel.DeleteAcc(IdUser);

            if (result != null)
            {
                return RedirectToAction("Index", new { IdUser = result.IdUser });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Absence()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Vacation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VacationAdd()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VacationRefund()
        {
            return View();
        }
    }
}
