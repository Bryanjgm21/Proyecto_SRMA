using Microsoft.AspNetCore.Mvc;
using SRMA.Interfaces;

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
        public IActionResult Absence()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EmployeeInfo()
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
