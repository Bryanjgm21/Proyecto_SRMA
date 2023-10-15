using Dapper;
using MySql.Data.MySqlClient;
using SRMA.Entities;
using SRMA.Interfaces;
using System.Data;
using static Dapper.SqlMapper;

namespace SRMA.Models
{
    public class ProductModel : IProductModel
    {
        private readonly IConfiguration _configuration;
        public ProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<ProductEntity> ListProducts()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<ProductEntity>("GetAllProducts",
                   
                    commandType: CommandType.StoredProcedure).ToList();

                if (data != null && data.Count > 0)
                {
                    return data;
                }

                return new List<ProductEntity>();
            }
        }

        public ProductEntity? GetProductById(long IdProduct)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("pIdProduct", IdProduct, DbType.Int32, ParameterDirection.Input);

                var data = connection.Query<ProductEntity>("GetProductById", parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault(); // Cambiar "NombreDelProcedimientoAlmacenado" por el nombre correcto.

                return data ?? new ProductEntity(); // Devuelve el producto encontrado o una instancia vacía si no se encuentra.
            }
        }

        public ProductEntity? InsertProduct(ProductEntity entity)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    connection.Execute("InsertProduct",
                       new { entity.productName, entity.details, entity.stock, entity.productStatus, entity.price, entity.IdSupplier },
                       commandType: System.Data.CommandType.StoredProcedure);

                    return entity;
                    
                }
            }
            else
            {
                return null;
            }
        }

        public ProductEntity? UpdateProduct(ProductEntity entity, long IdProduct)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var parameters = new
                    {
                        pIdProduct = IdProduct,
                        pProductName = entity.productName,
                        pDetails = entity.details,
                        pStock = entity.stock,
                        pProductStatus = entity.productStatus,
                        pPrice = entity.price,
                        pIdSupplier = entity.IdSupplier
                    };

                    var result = connection.Execute("UpdateProduct", parameters, commandType: CommandType.StoredProcedure);

                    return entity;
                }
            }
            else
            {
                return null;
            }
        }

        public ProductEntity? DeleteProduct(long IdProduct)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var parameters = new { pIdProduct = IdProduct };

                // Ejecuta el procedimiento almacenado o consulta SQL para eliminar el producto
                var result = connection.Execute("DeleteProduct", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    // La eliminación fue exitosa, puedes devolver una respuesta, por ejemplo, un mensaje de éxito.
                    return new ProductEntity { IdProduct = IdProduct };
                }
                else
                {
                    // En caso de que no se haya eliminado ningún producto (por ejemplo, si el IdProduct no existe), puedes devolver nulo o algún otro valor para indicar que la operación no fue exitosa.
                    return null;
                }
            }
        }


        public List<ProductEntity> GetUrgentProducts()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<ProductEntity>("GetUrgentProducts",
                    commandType: CommandType.StoredProcedure).ToList();

                if (data != null && data.Count > 0)
                {
                    return data;
                }

                return new List<ProductEntity>();
            }
        }

    }
}
