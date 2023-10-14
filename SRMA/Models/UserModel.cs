using Dapper;
using SRMA.Interfaces;
using SRMA.Entities;
using MySql.Data.MySqlClient;
using System.Data;
using MimeKit;

namespace SRMA.Models
{
    public class UserModel : IUserModel
    {
        private readonly IConfiguration _configuration;
        public UserModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // LogIn Method
        public UserEntity LogIn(UserEntity entity)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var result = connection.Query<UserEntity>("user_Validation",
                    new { entity.email, entity.passwordU },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                return result;
            }
        }

        // SignUp Method
        public UserEntity? RegisterUser(UserEntity entity)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    connection.Execute("RegisterUser",
                       new { entity.userName, entity.lastName, entity.Id, entity.email, entity.cellphone, entity.statusU, entity.passwordU, entity.IdRol },
                       commandType: System.Data.CommandType.StoredProcedure);

                    return entity;

                }
            }
            else
            {

                return null;
            }
        }
        
        // Consult data from all roles users, case Client(3) | q = IdUser
        public UserEntity? ConsultAcc(long q)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var data = connection.Query<UserEntity>("ConsultAcc",
                     new { IdUser = q },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (data != null)
                {

                    var userViewModel = new UserEntity
                    {
                        IdUser = data.IdUser,
                        userName = data.userName,
                        lastName = data.lastName,
                        Id = data.Id,
                        cellphone = data.cellphone,
                        email = data.email,
                        passwordU = data.passwordU,

                    };

                    return userViewModel;
                }

                return null;
            }
        }

        //Updates data from user by consulting their IdUser = q
        public UserEntity? UpdateUser(UserEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var result = connection.Execute("EditAcc",
                       new { IdUser =  q, entity.userName, entity.lastName, entity.cellphone, entity.email, entity.passwordU },
                       commandType: CommandType.StoredProcedure);

                    return entity;
                }
            }
            else
            {
                return null;
            }
        }

        // Deletes User by id
        public UserEntity? DeleteAcc(long q)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {

                connection.Execute("DeleteAcc",
                       new { IdUser = q },
                       commandType: System.Data.CommandType.StoredProcedure);

                return null;
            }

        }

        // Method to Register a user in the loyalty program, first consult the user id then registers the user in the loyalty program
        public UserEntity? RegUserProg(long q)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var user = connection.Query<UserEntity>("ConsultAcc",
                     new { IdUser = q },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (user != null)
                {

                    connection.Execute("RegUserProg",
                       new { IdUser = q },
                       commandType: System.Data.CommandType.StoredProcedure); ;

                    return user;
                }

                return null;
            }
        }

        // Validates if the email exist in the db
        public UserEntity? email_Verification(UserEntity entity)
        {

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                return connection.Query<UserEntity>("email_ExistenceVerification",
                    new { entity.email },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        // Send the email to recover the password in the db
        private string? email_Recovery(string p_Email)
        {

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                return connection.Query<string>("email_Recovery",
                    new { p_Email },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        // List of users by role

        public List<UserEntity> ListUsers(byte q)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var userList = connection.Query<UserEntity>("GetUsers",
                    new { pIdRol = q }, 
                    commandType: CommandType.StoredProcedure).ToList();

                if (userList != null && userList.Count > 0)
                {
                    return userList;
                }

                return new List<UserEntity>();
            }
        }



        // Mail body
        public void Email(string mail)
        {
            //aqui se consulta la contraseña
            var respuesta = email_Recovery(mail);

            //declaro las variables para llenar lo necesario del mailkit
            var ServerEmail = _configuration.GetSection("EmailConfiguration:ServerEmail").Value;
            var ServerPassword = _configuration.GetSection("EmailConfiguration:ServerPassword").Value;
            var Host = _configuration.GetSection("EmailConfiguration:Host").Value;
            var Port = _configuration.GetSection("EmailConfiguration:Port").Value;

            //rellenamos lo solicitado
            var mensaje = new MimeMessage();
            mensaje.From.Add(MailboxAddress.Parse(ServerEmail)); //el parse es solo para poner correos y evitar poner nombres
            mensaje.To.Add(MailboxAddress.Parse(mail));
            mensaje.Subject = "Su contraseña es la siguiente: ";
            mensaje.Body = new TextPart(MimeKit.Text.TextFormat.Html) //ponemos que el texto tendra formato html para darle valor al correo
            {
                Text = "<h1>Estimado/a " + mail + ",<h1>" +
                     "<p>Hemos recibido su solicitud de recuperación de contraseña para su cuenta</p>" +
                     "<p><strong>Correo electrónico : </strong>" + mail + "</p>" +
                     "<p><strong>Contraseña : </strong>" + respuesta + "</p>"
            };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(Host, int.Parse(Port), false);
            smtp.Authenticate(ServerEmail, ServerPassword);
            smtp.Send(mensaje);
            smtp.Disconnect(true);

        }

    }
}
