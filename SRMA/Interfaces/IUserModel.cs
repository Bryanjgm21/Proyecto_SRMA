using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IUserModel
    {
        // Login & SignUp Functions
        public UserEntity LogIn(UserEntity entity);
        public UserEntity? RegisterUser(UserEntity entity);

        // CRUD User
        public UserEntity? ConsultAcc(long q);
        public UserEntity? UpdateUser(UserEntity entity, long q);
        public UserEntity? DeleteAcc(long IdUser);
        public List<UserEntity> ListUsers(byte q);

        // Fidelity Program SignUp
        public UserEntity? RegUserProg(long q);

        // Password Recovery via Email
        public UserEntity? email_Verification(UserEntity entity);
        public void Email(string mail);

        public int RecoverAccount(UserEntity entity);
        public int ChangeAccPassword(UserEntity entity);
        public UserEntity? verifUser(UserEntity entity);

    }
}
