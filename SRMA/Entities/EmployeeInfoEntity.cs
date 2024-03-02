﻿using Humanizer;
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
        public bool statusU { get; set; }

        //FK Vacations and absence
        public long idVA { get; set; }
        public int dReq { get; set; }
        public DateTime inDay { get; set; }
        public DateTime enDay { get; set; } 
        public int typeV { get; set; }
        public int auType { get; set; }

    }
}
