using Microsoft.AspNetCore.Mvc;
using SRMA.Entities;    
using SRMA.Interfaces;
using SRMA.Models;

namespace SRMA.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductModel _productModel;

        public ProductController(IProductModel productModel)
        {
            _productModel = productModel;
        }

        [HttpGet]
        public IActionResult Index()
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
    }
}
