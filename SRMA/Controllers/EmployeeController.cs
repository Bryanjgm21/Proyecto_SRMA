using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using SRMA.Entities;
using SRMA.Interfaces;
using SRMA.Models;
using static Dapper.SqlMapper;

namespace SRMA.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUserModel _userModel;
        private readonly IEmployeeInfoModel _userEmployeeInfoModel;


        public EmployeeController(IUserModel userModel, IEmployeeInfoModel userEmployeeInfoModel)
        {
            _userModel = userModel;
            _userEmployeeInfoModel = userEmployeeInfoModel;
        }

       //List of Users by Rol

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userModel.ConsultInfoAllEmployees();
            return View(users);
        }
                
        [HttpGet]
        public IActionResult EmployeeInfo(long IdUser)
        {
           
            var result = _userModel.ConsultInfoEmployee(IdUser);

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

            var resultado = _userModel.RegisterEmployee(entity);

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
            var IdUser = entity.IdUser;

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

            var resultado = _userModel.UpdateEmployee(entity, entity.IdUser);

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


        [HttpGet]
        public IActionResult ChangeStatusEmployee(long q)
        {
            var entity = new UserEntity();
            entity.IdUser = q;

            _userModel.ChangeStatusEmployee(q);
            return RedirectToAction("Index");
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

        //public IActionResult AddInfoEmp(EmployeeInfoEntity entity, long q)
        //{
            
        //    if (!ModelState.IsValid)
        //    {
        //         return View("AddInfoEmp", entity);
        //    }

        //    var ver = _userEmployeeInfoModel.AddInfoEmp(entity, q);
        //    if (ver != null)
        //    {
        //       TempData["RegistroExitoso"] = "Se registro correctamente.";

        //        return RedirectToAction("EmployeeInfo");
        //    }
        //    else
        //    {
        //        TempData["MensajeError"] = "Error al registrar el usuario.";
        //    }

        //    return RedirectToAction("EmployeeInfo");
        //}

        //public IActionResult UpdateInfoEmp(EmployeeInfoEntity entity)
        //{
        //    var IdUser = entity.IdUser;

        //    if (!ModelState.IsValid)
        //    {
        //        return View("ActInfoEmp", entity);
        //    }

        //    var resultado = _userEmployeeInfoModel.UpdateInfoEmp(entity, IdUser);
            
        //    if (resultado != null)
        //    {
        //        ViewBag.MensajeExito = "Actualizacion exitosa.";
               
        //        ModelState.AddModelError("RegistroExitoso", "Actualizacion exitosa");

        //        return View("EmployeeInfo", entity);
        //    }
        //    else
        //    {
        //       ViewBag.MensajeError = "Error al actualizar el usuario.";
        //    }

        //    return View("EmployeeInfo", entity);
        //}

        public IActionResult ConsultInfoE(long IdUser)
        {

            var result = _userEmployeeInfoModel.ConsultInfoE(IdUser);

            if (result != null)
            {
                return View(result);
            }

            return View(result); 
        }

        [HttpGet]
        public IActionResult Absence()
        {
            {
                var absence = _userEmployeeInfoModel.ConsultVacAu(false);
                return View(absence);

            }
        }

        [HttpGet]
        public IActionResult AbsenceAdd(long IdUser)
        {
            var result = _userEmployeeInfoModel.ConsultInfoE(IdUser);

            if (result != null)
            {
                return View(result);
            }

            return View(); // Maneja el caso en el que el usuario no se encuentre
        }

        [HttpPost]
        public IActionResult AbsenceAdd(EmployeeInfoEntity entity)
        {
            long q = entity.IdUser;
            entity.typeV = 0;
            entity.auType = 0;

            if (!ModelState.IsValid)
            {
                if (entity.enDay == DateTime.MinValue && entity.enDay == DateTime.MinValue)
                {
                    ViewBag.MsjError = "La fecha de inicio y finalización son obligatorias.";
                    return View("AbsenceAdd", entity);
                }
                else
                {
                    return View("AbsenceAdd", entity);
                }
                
            }

            var ver = _userEmployeeInfoModel.AddAu(entity, q);
            if (ver != null)
            {
                TempData["RegistroExitoso"] = "Se registro correctamente.";

                return RedirectToAction("AbsenceAdd");
            }
            else
            {
                TempData["MensajeError"] = "Error al registrar la ausencia.";
            }

            return RedirectToAction("AbsenceAdd");
        }

        [HttpGet]
        public IActionResult AbsenceEdit()
        {
            {

                var absence = _userEmployeeInfoModel.ConsultVacAu(false);
                return View(absence);

            }
        }



        [HttpGet]
        public IActionResult Vacation()
        {
            var vacation = _userEmployeeInfoModel.ConsultVacAu(true);
            return View(vacation);
          
        }

        [HttpGet]
        public IActionResult VacationAdd(long IdUser)
        {
            var result = _userEmployeeInfoModel.ConsultInfoE(IdUser);

            if (result != null)
            {
                return View(result);
            }

            return View();
        }

        [HttpPost]
        public IActionResult VacationAdd(EmployeeInfoEntity entity)
        {
            long q = entity.IdUser;
            entity.typeV = 1;
            entity.auType = 0;

            if (entity.ptoDays < entity.dReq)
            {
                ViewBag.ErrorMessage = "No cuenta con suficientes dias para realizar la solicitud";
                return View("VacationAdd", entity);
            }
            

            if (!ModelState.IsValid)
            {
                if (entity.enDay == DateTime.MinValue && entity.enDay == DateTime.MinValue)
                {
                    ViewBag.MsjError = "La fecha de inicio y finalización son obligatorias.";
                    return View("VacationAdd", entity);
                }
                else
                {
                    return View("VacationAdd", entity);
                }
            }

            var ver = _userEmployeeInfoModel.AddAu(entity,q);
            if (ver != null)
            {
                TempData["RegistroExitoso"] = "Se registro correctamente.";

                return RedirectToAction("VacationAdd");
            }
            else
            {
                TempData["MensajeError"] = "Error al registrar la solicitud.";
            }

            return RedirectToAction("VacationAdd");
        }
    

        [HttpPost]
       
        public IActionResult DeleteRequestVac(long idVa)
        {
            int type = 1;

            var result = _userEmployeeInfoModel.DeleteRequest(idVa,type);

            if (result != null)
            {
                return RedirectToAction("Vacation");
            }
            else
            {
                return RedirectToAction("Vacation");
            }
        }

        [HttpPost]

        public IActionResult DeleteRequestAbs(long idVa)
        {
            int type = 0;

            var result = _userEmployeeInfoModel.DeleteRequest(idVa,type);

            if (result != null)
            {
                return RedirectToAction("Absence");
            }
            else
            {
                return RedirectToAction("Absence");
            }
        }
    }
}
