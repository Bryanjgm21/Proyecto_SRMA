using Dapper;
using SRMA.Interfaces;
using SRMA.Entities;
using MySql.Data.MySqlClient;
using System.Data;
using MimeKit;


namespace SRMA.Models
{
    public class EmployeeModel : IEmployeeModel
    {
        private readonly IConfiguration _configuration;
        public EmployeeModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Add employee Method
        public EmployeeEntity? AddEmplInfo(EmployeeEntity employee, UserEntity user)
        {
            if (employee != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    connection.Execute("Add_EmplInfo",
                       new { employee.IdEmpl, employee.salary, employee.workHours, employee.jobTitle,vacations = user.ptoDays, employee.absences, user.startDate},
                       commandType: System.Data.CommandType.StoredProcedure);

                    return employee;

                }
            }
            else
            {

                return null;
            }
        }


    }
}
