using Humanizer;
using Org.BouncyCastle.Ocsp;

namespace SRMA.Entities
{
    public class EmployeeInfoEntity
    {
        public long IdE { get; set; }
        public int salary { get; set; }
        public string job { get; set; } = string.Empty;
        public string scheduleE { get; set; } = string.Empty;
        public DateOnly startDate { get; set; }
        public int ptoDays { get; set; }

        //FK users
        public long IdUser { get; set; }
        public string userName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        //FK Vacations and absence
        public int DReq { get; set; }
        public DateOnly InDay { get; set; }
        public DateOnly EnDay { get; set; } 
        public bool TypeV { get; set; }
        public bool AuType { get; set; }

    }
}
