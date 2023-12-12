using SRMA.Interfaces;
using SRMA.Entities;
using MySql.Data.MySqlClient;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace SRMA.Models
{
    public class LogBookModel : ILogBookModel
    {
        private readonly IConfiguration _configuration;
        private string _connection;
        public LogBookModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("defaultconnection");
        }

        public int RegisterLogBook(LogBookEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_connection))
                {
                    var data = connection.Execute("InsertLogBook",
                       new
                       {   IdUser = q,
                           message = entity.message,
                           origin = entity.origin,
                           log_type = entity.log_type                           
                       },
                       commandType: System.Data.CommandType.StoredProcedure);

                    return 1;

                }
            }
            else
            {

                return 0;
            }
        }

    }
}
