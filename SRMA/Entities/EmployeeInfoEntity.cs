using Humanizer;
using Org.BouncyCastle.Ocsp;
using System.ComponentModel.DataAnnotations;

namespace SRMA.Entities
{
    public class EmployeeInfoEntity
    {
        public long IdE { get; set; }
        public int salary { get; set; }
        public string job { get; set; } = string.Empty;
        public string scheduleE { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public int ptoDays { get; set; }

        //FK users
        public long IdUser { get; set; }
        public string userName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;

        //FK Vacations and absence
        public long idVA { get; set; }
        public int dReq { get; set; }
        public DateOnly inDay { get; set; }
        public DateOnly enDay { get; set; } 
        public bool typeV { get; set; }
        public bool auType { get; set; }

    }
}
