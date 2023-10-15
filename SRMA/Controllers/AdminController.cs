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

        public AdminController(IProductModel productModel)
        {
            _productModel = productModel;
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
            return View();
        }
    }
}
