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
        public EmployeeInfoEntity? UpdateInfoEmp(EmployeeInfoEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var result = connection.Execute("UpdateInfoEmp",
                       new { IdUser = q, entity.salary, entity.job, entity.scheduleE, entity.startDate,entity.ptoDays},
                       commandType: CommandType.StoredProcedure);

                    return entity;
                }
            }
            else
            {
                return null;
            }
        }

        public EmployeeInfoEntity? ConsultInfoE(long q)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<EmployeeInfoEntity>("ConsultInfoE",
                     new { pIdUser = q },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (data != null)
                {

                    var EmployeeViewModel = new EmployeeInfoEntity
                    {
                        IdUser = data.IdUser,
                        userName = data.userName,
                        lastName = data.lastName,
                        Id = data.Id,
                        salary=data.salary,
                        job=data.job,
                        scheduleE = data.scheduleE,
                        startDate = data.startDate,
                        ptoDays = data.ptoDays,
                        IdE = data.IdE
                    };

                    return EmployeeViewModel;
                }

                return null;
            }
        }

        public EmployeeInfoEntity? AddAu(EmployeeInfoEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    connection.Execute("AddAu",
                       new { pIdUser = q, entity.dReq, entity.inDay, entity.enDay, entity.typeV, entity.auType },
                       commandType: System.Data.CommandType.StoredProcedure);

                    return entity;

                }
            }
            else
            {

                return null;
            }
        }

        public EmployeeInfoEntity? DeleteRequest(long IdReq)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var parameters = new { pIdVA = IdReq };

                // Ejecuta el procedimiento almacenado o consulta SQL para eliminar el producto
                var result = connection.Execute("DeleteRequest", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    // La eliminación fue exitosa, puedes devolver una respuesta, por ejemplo, un mensaje de éxito.
                    return new EmployeeInfoEntity { idVA = IdReq };
                }
                else
                {
                    // En caso de que no se haya eliminado ningún producto (por ejemplo, si el IdProduct no existe), puedes devolver nulo o algún otro valor para indicar que la operación no fue exitosa.
                    return null;
                }
            }
        }

        public List<EmployeeInfoEntity> ConsultVacAu(bool q)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<EmployeeInfoEntity>("ConsultVacAu",
                   new { ptypeV = q },
                    commandType: CommandType.StoredProcedure).ToList();

                if (data != null && data.Count > 0)
                {
                    return data;
                }

                return new List<EmployeeInfoEntity> ();
            }
        }

        //public EmployeeInfoEntity? ConsultVacAu(bool q)

        //{
        //    using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
        //    {
        //        var data = connection.Query<EmployeeInfoEntity>("ConsultVacAu",
        //             new { ptypeV = q },
        //             commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

        //        if (data != null)
        //        {

        //            var EmployeeViewModel = new EmployeeInfoEntity
        //            {
        //                IdUser = data.IdUser,
        //                userName = data.userName,
        //                lastName = data.lastName,
        //                ptoDays = data.ptoDays,
        //                idVA = data.idVA,
        //                inDay = data.inDay,
        //                enDay = data.enDay,
        //                dReq = data.dReq,
        //                auType = data.auType,
        //                typeV=data.typeV,
        //            };

        //            return EmployeeViewModel;
        //        }

        //        return null;
        //    }
        //}



    }
}
