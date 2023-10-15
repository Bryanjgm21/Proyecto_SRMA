using Dapper;
using MySql.Data.MySqlClient;
using SRMA.Entities;
using SRMA.Interfaces;
using System.Data;

namespace SRMA.Models
{
    public class SupplierModel : ISupplierModel
    {
        private readonly IConfiguration _configuration;

        public SupplierModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<SupplierEntity> ListSuppliers()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<SupplierEntity>("GetAllSuppliers", commandType: CommandType.StoredProcedure).ToList();

                if (data != null && data.Count > 0)
                {
                    return data;
                }

                return new List<SupplierEntity>();
            }
        }
    }
}
