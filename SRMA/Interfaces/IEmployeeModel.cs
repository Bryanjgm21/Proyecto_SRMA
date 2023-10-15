using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IEmployeeModel
    {
        //CRUD Employee Info
        public EmployeeEntity? AddEmplInfo(EmployeeEntity employee, UserEntity user);

    }
}
