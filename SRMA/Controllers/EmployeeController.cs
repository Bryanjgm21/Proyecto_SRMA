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
            entity.passwordU = "password1";

            if (!ModelState.IsValid)
            {
                return View("Create",entity);
            }

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


        [HttpPost]
        public IActionResult Edit(UserEntity entity)
        {
            if (!ModelState.IsValid)
            {
                return View("EmployeeInfo", entity);
            }

            var resultado = _userModel.UpdateUser(entity, entity.IdUser);


            if (resultado != null)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return RedirectToAction("EmployeeInfo", "Employee");
            }
        }


        [HttpPost]
        public IActionResult DeleteAcc(int IdUser)
        {
           
            long UserId = (long)IdUser;

            var result = _userModel.DeleteAcc(UserId);

            if (result != null)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return RedirectToAction("Index", "Employee");
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
