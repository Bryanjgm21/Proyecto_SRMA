using Microsoft.AspNetCore.Mvc;
using SRMA.Entities;
using SRMA.Interfaces;

namespace SRMA.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierModel _supplierModel;

        public SupplierController(ISupplierModel supplierModel)
        {
            _supplierModel = supplierModel;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ListSuppliers()
        {
            List<SupplierEntity> suppliers = _supplierModel.ListSuppliers();
            return View(suppliers);
        }
    }
}
