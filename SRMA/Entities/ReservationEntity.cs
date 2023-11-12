using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace SRMA.Entities
{

    public class ReservationEntity
    {

       
        // Main atributes
        public long IdReservation { get; set; }
        public DateOnly dateR { get; set; }
        public TimeOnly timeR{ get; set; }
        public DateTime dateReservation { get; set; }

        public int quantity { get; set; }

        public bool statusReser { get; set; }

        public string details { get; set; } = string.Empty;

        public string observations { get; set; } = string.Empty;

        // FK Table Users
        public int IdUser { get; set; }
        public string email { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;


    }
}
