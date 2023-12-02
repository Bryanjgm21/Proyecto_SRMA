using Dapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using SRMA.Entities;    
using SRMA.Interfaces;
using SRMA.Models;
using System.Data;
using System.Diagnostics;

namespace SRMA.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductModel _productModel;
        private readonly ISupplierModel _supplierModel;

        public ProductController(IProductModel productModel, ISupplierModel supplierModel)
        {
            _productModel = productModel;
            _supplierModel = supplierModel;
        }

        // Consult all products to show the data
        [HttpGet]
        public IActionResult Index()
        {
            var data = _productModel.ListProducts();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var suppliers = _supplierModel.ListSuppliers();
            var listSuppliers = suppliers.Select(item => new SelectListItem { Text = item.supplierName, Value = item.IdSupplier.ToString() }).ToList();
            ViewBag.listSuppliers = listSuppliers;

            return View();
        }

        //Action that makes the Product Insertion
        [HttpPost]
        public IActionResult InsertProduct(ProductEntity entity)
        {
            if (!ModelState.IsValid)
            {
                var suppliers = _supplierModel.ListSuppliers();
                var listSuppliers = suppliers.Select(item => new SelectListItem { Text = item.supplierName, Value = item.IdSupplier.ToString() }).ToList();
                ViewBag.listSuppliers = listSuppliers;

                return View("Create", entity);
            }

            
            var resultado = _productModel.InsertProduct(entity);

            if (resultado != null)
            {
                TempData["RegistroExitoso"] = "El producto se agrego correctamente.";
                return RedirectToAction("Create");
            }
            else
            {
                TempData["MensajeError"] = "Error al agregar el producto. Por favor, inténtelo de nuevo.";
            }

            return RedirectToAction("Create");
        }


        // Consult the product by their id to update the data
        [HttpGet]
        public IActionResult Edit(long IdProduct)
        {

            // Aquí deberías obtener el producto que deseas editar utilizando _productModel.
            var product = _productModel.GetProductById(IdProduct);

            var suppliers = _supplierModel.ListSuppliers();

            var listSuppliers = new List<SelectListItem>();

            if (product == null)
            {
                return NotFound(); // Producto no encontrado
            }
            
            foreach (var item in suppliers)
            {
                listSuppliers.Add(new SelectListItem { Text = item.supplierName, Value = item.IdSupplier.ToString() });
            }
            ViewBag.listSuppliers = listSuppliers;

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductEntity model)
        {
            var suppliers = _supplierModel.ListSuppliers();
            var listSuppliers = suppliers.Select(item => new SelectListItem { Text = item.supplierName, Value = item.IdSupplier.ToString() }).ToList();
            ViewBag.listSuppliers = listSuppliers;

            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var resultado = _productModel.UpdateProduct(model, model.IdProduct);

            if (resultado != null)
            {
                TempData["RegistroExitoso"] = "El producto se modifico correctamente.";
                return RedirectToAction("Edit");
            }
            else
            {
                TempData["MensajeError"] = "Error al modificar el producto. Por favor, inténtelo de nuevo.";
            }

            return RedirectToAction("Edit", new { id = model.IdProduct });
        }

        [HttpPost]
        public IActionResult Delete(long IdProduct)
        {
            // Llama a la función DeleteProduct en el modelo de productos para eliminar el producto
            var deletedProduct = _productModel.DeleteProduct(IdProduct);

            if (deletedProduct != null)
            {
                // Si el producto se eliminó correctamente, puedes redirigir a una página de éxito o realizar otras acciones necesarias.
                return RedirectToAction("Index", new { IdProduct = deletedProduct.IdProduct });
            }
            else
            {
                // Si la eliminación no fue exitosa (por ejemplo, el producto no existe), puedes redirigir a una página de error o realizar otras acciones necesarias.
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult ActivProd(long IdProduct)
        {
            // Llama a la función DeleteProduct en el modelo de productos para eliminar el producto
            var deletedProduct = _productModel.ActivProduct(IdProduct);

            if (deletedProduct != null)
            {
                // Si el producto se eliminó correctamente, puedes redirigir a una página de éxito o realizar otras acciones necesarias.
                return RedirectToAction("Index", new { IdProduct = deletedProduct.IdProduct });
            }
            else
            {
                // Si la eliminación no fue exitosa (por ejemplo, el producto no existe), puedes redirigir a una página de error o realizar otras acciones necesarias.
                return RedirectToAction("Index");
            }
        }

        // 
        [HttpGet]
        public async Task<FileResult> ExportProductsToExcel()
        {
            var data = _productModel.ListProducts();
            var fileName = $"InformeInvProductos_{DateTime.Now.ToString("yyyy/MM/dd")}.xlsx";
            return GenerateExcelProducts(fileName, data);

        }

        private FileResult GenerateExcelProducts(string fileName, IEnumerable<ProductEntity> products)
        {

            DataTable dataTable = new DataTable("Products");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("IdProduct"),
                new DataColumn("productName"),
                new DataColumn("stock"),
                new DataColumn("details"),
                new DataColumn("productStatus"),
                new DataColumn("price")


            });
            foreach (var data in products)
            {
                dataTable.Rows.Add(data.IdProduct,
                                    data.productName,
                                    data.stock,
                                    data.details,
                                    data.productStatus,
                                    data.price);

            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            fileName);

                }
            }


        }
    }

}
