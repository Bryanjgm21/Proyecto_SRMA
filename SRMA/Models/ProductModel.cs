using Dapper;
using MySql.Data.MySqlClient;
using SRMA.Entities;
using SRMA.Interfaces;
using System.Data;

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
    }
}
