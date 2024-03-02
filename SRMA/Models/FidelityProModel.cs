using Dapper;
using SRMA.Interfaces;
using SRMA.Entities;
using MySql.Data.MySqlClient;
using System.Data;

namespace SRMA.Models
{
    public class FidelityProModel : IFidelityProModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilities _utilities;
        private string _connection;

        public FidelityProModel(IConfiguration configuration, IUtilities utilities)
        {
            _configuration = configuration;
            _utilities = utilities;
            _connection = _configuration.GetConnectionString("defaultconnection");
        }

        public FidelityProEntity? ConsultPoints(long q)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<FidelityProEntity>("ConsultPoints",
                     new { pIdUser = q },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (data != null)
                {

                    var FidelityProViewModel = new FidelityProEntity
                    {
                        IdUser = data.IdUser,
                        IdProgram = data.IdProgram,
                        earnedPoints = data.earnedPoints,
                        codeU = data.codeU,
                        startDate = data.startDate,
                    };

                    return FidelityProViewModel;
                }

                return null;
            }
        }

        public int InsertPoints(long q, int pQty)
        {
            if (q != 0 && pQty != 0)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var res= connection.Execute("InsertPoints",
                       new { pIdUser = q, qty = pQty },
                       commandType: CommandType.StoredProcedure);
                   
                    return res;
                }
            }
            else
            {
                return 0;
            }
            
        }

        public FidelityProEntity? RedeemPoints(string Code, int pQty)
        {
            if (Code != null && pQty != 0)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var result = connection.Execute("RedeemPoints",
                       new { pCode = Code, qty = pQty },
                       commandType: CommandType.StoredProcedure);
                    if (result == 1)
                    {
                        var updatedUser = connection.QueryFirstOrDefault<FidelityProEntity>("SELECT * FROM fidelityProgram WHERE codeU = @Code",
                            new { code = Code }
                        );

                        return updatedUser;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            else
            {
                return null;
            }
        }

        public List<FidelityProEntity> GetAllFidelity()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<FidelityProEntity>("GetAllFidelity",

                    commandType: CommandType.StoredProcedure).ToList();

                if (data != null && data.Count > 0)
                {
                    return data;
                }

                return new List<FidelityProEntity>();
            }
        }


    }
}
