﻿using Dapper;
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
        public int InsertReservationByClient(ReservationEntity entity)

        {
            using (var connection = new MySqlConnection(_connection))
            {
                var user = connection.Query<UserEntity>("ConsultAcc",
                     new { IdUser = entity.IdUser },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (user != null)
                {

                    connection.Execute("InsertReservation",
                       new { entity.quantity, entity.details, entity.observations, entity.dateReservation, entity.IdUser, },
                       commandType: System.Data.CommandType.StoredProcedure); ;

                    return 1;
                }

                return 0;
            }
        }

    }
}
