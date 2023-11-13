using Dapper;
using SRMA.Interfaces;
using SRMA.Entities;
using MySql.Data.MySqlClient;
using System.Data;
using MimeKit;
using static Dapper.SqlMapper;

namespace SRMA.Models
{
    public class ReservationModel : IReservationModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilities _utilities;
        private string _connection;

        public ReservationModel(IConfiguration configuration, IUtilities utilities)
        {
            _configuration = configuration;
            _utilities = utilities;
            _connection = _configuration.GetConnectionString("defaultconnection");

        }

        public List<ReservationEntity> GetReservationsByUser(long q)
        {
            using (var connection = new MySqlConnection(_connection))
            {
                var parameters = new
                {
                    pIdUser = q
                };

                var data = connection.Query<ReservationEntity>("GetReservationsByUser",
                    parameters,
                    commandType: CommandType.StoredProcedure).ToList();

                return data ?? new List<ReservationEntity>();
            }
        }

        // Method to Register a user in the loyalty program, first consult the user id then registers the user in the loyalty program
        public int InsertReservationByClient(ReservationEntity entity, long q)

        {
            using (var connection = new MySqlConnection(_connection))
            {
                var user = connection.Query<UserEntity>("ConsultAcc",
                     new { IdUser = q },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (user != null)
                {

                    connection.Execute("InsertReservation",
                       new { entity.quantity, entity.details, entity.observations, dateR = entity.dateReservation.Date, timeR = entity.dateReservation.TimeOfDay, IdUser = q},
                       commandType: System.Data.CommandType.StoredProcedure);

                    return 1;
                }

                return 0;
            }
        }

        public List<ReservationEntity> ListReservations()
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var reservationsList = connection.Query<ReservationEntity>("GetAllReservations",
                    commandType: CommandType.StoredProcedure).ToList();

                if (reservationsList != null && reservationsList.Count > 0)
                {
                    return reservationsList;
                }

                return new List<ReservationEntity>();
            }
        }

        public ReservationEntity? GetReservationsById(long q)

        {
            using (var connection = new MySqlConnection(_connection))
            {
                var data = connection.Query<ReservationEntity>("GetReservationById",
                     new { pIdReservation = q },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (data != null)
                {

                    var reservationViewModel = new ReservationEntity
                    {
                        IdUser = data.IdUser,
                        userName = data.userName,
                        lastName = data.lastName,
                        IdReservation = data.IdReservation,
                        details = data.details,
                        observations = data.observations,
                        quantity = data.quantity,
                        dateReservation = data.dateReservation,
                    };

                    return reservationViewModel;
                }

                return null;
            }
        }
             public ReservationEntity? UpdateReservation(ReservationEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_connection))
                {
                    var result = connection.Execute("UpdateReservation",
                       new { pIdReservation = q, entity.quantity, entity.details, entity.observations, entity.dateReservation,entity.IdUser},
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
