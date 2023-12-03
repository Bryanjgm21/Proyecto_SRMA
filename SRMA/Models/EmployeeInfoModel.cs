using Dapper;
using SRMA.Interfaces;
using SRMA.Entities;
using MySql.Data.MySqlClient;
using System.Data;

namespace SRMA.Models
{
    public class EmployeeInfoModel: IEmployeeInfoModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilities _utilities;
        private string _connection;

        public EmployeeInfoModel(IConfiguration configuration, IUtilities utilities)
        {
            _configuration = configuration;
            _utilities = utilities;
            _connection = _configuration.GetConnectionString("defaultconnection");
        }
        public EmployeeInfoEntity? AddInfoEmp(EmployeeInfoEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    connection.Execute("AddInfoEmp",
                       new { pIdUser=q, entity.salary, entity.job,  entity.scheduleE, entity.startDate, entity.ptoDays},
                       commandType: System.Data.CommandType.StoredProcedure);

                    return entity;

                }
            }
            else
            {

                return null;
            }
        }
        public EmployeeInfoEntity? ActInfoEmp(EmployeeInfoEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var result = connection.Execute("ActInfoEmp",
                       new { IdUser = q, entity.userName, entity.lastName,},
                       commandType: CommandType.StoredProcedure);

                    return entity;
                }
            }
            else
            {
                return null;
            }
        }


    }
}
