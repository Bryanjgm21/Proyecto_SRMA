using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]

        public async Task<FileResult> ExportProductsToExcel()
        { 
            var data =  _productModel.ListProducts();
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
