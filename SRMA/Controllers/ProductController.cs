using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SRMA.Entities;    
using SRMA.Interfaces;
using SRMA.Models;
using System.Data;

namespace SRMA.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductModel _productModel;

        public ProductController(IProductModel productModel)
        {
            _productModel = productModel;
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
            return View();
        }

        //Action that makes the Product Insertion
        [HttpPost]
        public IActionResult InsertProduct(ProductEntity entity)
        {
           
            var resultado = _productModel.InsertProduct(entity);

            if (resultado != null)
            {
                System.Threading.Thread.Sleep(3000);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return RedirectToAction("Create", "Product");
            }
        }

        // Consult the product by their id to update the data
        [HttpGet]
        public IActionResult Edit(long IdProduct)
        {
            // Aquí deberías obtener el producto que deseas editar utilizando _productModel.
            var product = _productModel.GetProductById(IdProduct);

            if (product == null)
            {
                return NotFound(); // Producto no encontrado
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductEntity model)
        {
            if (ModelState.IsValid)
            {
                // Realiza la actualización del producto en la base de datos a través de _productModel.
                _productModel.UpdateProduct(model, model.IdProduct);
                System.Threading.Thread.Sleep(3000);
                return RedirectToAction("Index"); // Redirige a la lista de productos u otra página según tus necesidades.
            }

            // Si el modelo no es válido, vuelve a mostrar la vista de edición con errores.
            return View(model);
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
    }
}
