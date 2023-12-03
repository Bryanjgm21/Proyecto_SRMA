using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SRMA.Entities;
using SRMA.Interfaces;
using SRMA.Models;
using System.Data;

namespace SRMA.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductModel _productModel;
        private readonly IUserModel _userModel;

        public AdminController(IProductModel productModel, IUserModel userModel)
        {
            _productModel = productModel;
            _userModel = userModel;

        }






        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminDashboard()
        {
            // Llamar al procedimiento almacenado para obtener los productos urgentes
            var urgentProducts = _productModel.GetUrgentProducts();

            ViewData["UrgentProducts"] = urgentProducts;

            return View();
        }




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


            var resultado = _userModel.UpdateUser(entity, UserId);
            Thread.Sleep(5000);

            if (resultado != null)
            {
                return RedirectToAction("AdminDashboard", "Admin");
            }
            else
            {
                return RedirectToAction("Profile", "Admin");
            }
        }
    }
}