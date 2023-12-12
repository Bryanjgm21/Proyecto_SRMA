using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IEmployeeInfoModel
    {
        public EmployeeInfoEntity? AddInfoEmp(EmployeeInfoEntity entity, long q);
        //public EmployeeInfoEntity? UpdateInfoEmp(EmployeeInfoEntity entity, long q);
        public EmployeeInfoEntity? ConsultInfoE(long q);
        public EmployeeInfoEntity? AddAu(EmployeeInfoEntity entity, long q);
        public EmployeeInfoEntity? DeleteRequest(long IdReq,int type);
        public List<EmployeeInfoEntity> ConsultVacAu(bool q);


    }
}
