using Dapper;
using SRMA.Interfaces;
using SRMA.Entities;
using MySql.Data.MySqlClient;
using System.Data;
using MimeKit;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SRMA.Models
{
    public class UserModel : IUserModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilities _utilities;
        private string _connection;
        public UserModel(IConfiguration configuration, IUtilities utilities)
        {
            _configuration = configuration;
            _utilities = utilities;
            _connection = _configuration.GetConnectionString("defaultconnection");
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

        // SignUp Method
        public UserEntity? RegisterEmployee(UserEntity entity)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_connection))
                {
                    connection.Execute("RegisterEmployee",
                       new { entity.userName, entity.lastName, entity.Id, entity.email, entity.cellphone, entity.salary, entity.job, entity.scheduleE, entity.ptoDays },
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
                        IdProgram = data.IdProgram,
                    };

                    return userViewModel;
                }

                return null;
            }
        }

        public UserEntity? ConsultInfoEmployee(long q)

        {
            using (var connection = new MySqlConnection(_connection))
            {
                var data = connection.Query<UserEntity>("ConsultInfoEmployee",
                     new { pIdUser = q },
                     commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (data != null)
                {

                    var EmployeeViewModel = new UserEntity
                    {
                        IdUser = data.IdUser,
                        userName = data.userName,
                        lastName = data.lastName,
                        cellphone = data.cellphone,
                        email = data.email,
                        Id = data.Id,
                        salary = data.salary,
                        job = data.job,
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

        public UserEntity? UpdateUser(UserEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var result = connection.Execute("EditAcc",
                       new { IdUser = q, entity.userName, entity.lastName, entity.cellphone, entity.email, entity.passwordU },
                       commandType: CommandType.StoredProcedure);

                    return entity;
                }
            }
            else
            {
                return null;
            }
        }

        //Updates data from user by consulting their IdUser = q
        public UserEntity? UpdateEmployee(UserEntity entity, long q)
        {
            if (entity != null)
            {
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
                {
                    var result = connection.Execute("EditAcc",
                       new { IdUser =  q, entity.userName, entity.lastName, entity.cellphone, entity.email, entity.passwordU },
                       commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {

                        connection.Execute("UpdateInfoEmp",
                           new { pIdUser = q, pSalary= entity.salary, pJob = entity.job, pScheduleE = entity.scheduleE, pPtoDays=entity.ptoDays },
                           commandType: System.Data.CommandType.StoredProcedure); ;

                        return entity;
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

        // Deletes User by id
        public UserEntity? DeleteAcc(long IdUser)

        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var parameters = new { UIdUser = IdUser };

                // Ejecuta el procedimiento almacenado o consulta SQL para eliminar el producto
                var result = connection.Execute("DeleteAcc", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    // La eliminación fue exitosa, puedes devolver una respuesta, por ejemplo, un mensaje de éxito.
                    return new UserEntity { IdUser = IdUser };
                }
                else
                {
                    // En caso de que no se haya eliminado ningún producto (por ejemplo, si el IdProduct no existe), puedes devolver nulo o algún otro valor para indicar que la operación no fue exitosa.
                    return null;
                }
            }
        }

        public ProductEntity? ActivProduct(long IdProduct)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("defaultconnection")))
            {
                var parameters = new { pIdProduct = IdProduct };
                var result = connection.Execute("ActiveProduct", parameters, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    // La eliminación fue exitosa, puedes devolver una respuesta, por ejemplo, un mensaje de éxito.
                    return new ProductEntity { IdProduct = IdProduct };
                }
                else
                {
                    // En caso de que no se haya eliminado ningún producto (por ejemplo, si el IdProduct no existe), puedes devolver nulo o algún otro valor para indicar que la operación no fue exitosa.
                    return null;
                }
            }
        }

        public int ChangeStatusEmployee(long q)
        {

            using (var connection = new MySqlConnection(_connection))
            {
                var parameters = new { pIdUser = q };

                var data = connection.Execute("ChangeStatusEmployee",
                    parameters, commandType: CommandType.StoredProcedure);

                return data;
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
                       new { pIdUser = q },
                       commandType: System.Data.CommandType.StoredProcedure); ;

                    return user;
                }

                return null;
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

        public List<UserEntity> ConsultInfoAllEmployees()
        {
            using (var connection = new MySqlConnection(_connection))
            {
                var data = connection.Query<UserEntity>("ConsultInfoAllEmployees",

                    commandType: CommandType.StoredProcedure).ToList();

                if (data != null && data.Count > 0)
                {
                    return data;
                }

                return new List<UserEntity>();
            }
        }

        //RecoverPW

        public int RecoverAccount(UserEntity entity)
        {
            using (var connection = new MySqlConnection(_connection))
            {
                var data = connection.Query<UserEntity>("RecoverAccount",
                        new { entity.email },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (data != null)
                {
                    string tempPassword = _utilities.GenerarCodigo();
                    string content = _utilities.ArmarHTML(data, tempPassword);

                    connection.Execute("UpdateTempKey",
                        new { data.IdUser, tempPassword },
                        commandType: CommandType.StoredProcedure);

                    _utilities.EnviarCorreo(data.email, "Restaurar Contraseña", content);
                    return 1;
                }
                else
                    return 0;
            }
        }

        public int ChangeAccPassword(UserEntity entity)
        {
            using (var connection = new MySqlConnection(_connection))
            {
                entity.IdUser = long.Parse(_utilities.Decrypt(entity.IdUserEncrypt));

                var data = connection.Execute("ChangeAccPassword",
                    new { entity.IdUser, entity.tempPassword, entity.passwordU },
                    commandType: CommandType.StoredProcedure);

                return 1;
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

        public UserEntity? verifUser(UserEntity entity, long q)
        {

            using (var connection = new MySqlConnection(_connection))
            {
                return connection.Query<UserEntity>("verifyEmail",
                    new { pEmail = entity.email, pId=q},
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public UserEntity? verCed(UserEntity entity, long q)
        {

            using (var connection = new MySqlConnection(_connection))
            {
                return connection.Query<UserEntity>("verifyId",
                    new { pId = entity.Id, pIdUser = q },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}
