using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.ComponentModel.DataAnnotations;

namespace SRMA.Entities
{

    public class ReservationEntity
    {

       
        // Main atributes
        public long IdReservation { get; set; }
        public DateOnly dateR { get; set; }
        public TimeOnly timeR{ get; set; }
         public DateTime dateReservation { get; set; }

        [Required(ErrorMessage = "El campo cantidad es obligatorio.")]
        [Range(1, 15, ErrorMessage = "El valor debe estar entre 1 y 15.")]
        [RegularExpression(@"^[1-9]\d{0,1}$", ErrorMessage = "Debe solo ser solo números")]
        public int? quantity { get; set; }

        public bool statusReser { get; set; }

        //public string details { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo observaciones es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo observaciones no puede tener más de 100 caracteres.")]
        public string observations { get; set; } = string.Empty;

        // FK Table Users
        public int IdUser { get; set; }
        public string email { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;

        // New property to track if attendance is confirmed
        public bool AttendanceConfirmed { get; set; }

    }
}
