using System.ComponentModel.DataAnnotations;

namespace SRMA.Entities
{
    public class UserEntity
    {

        public long IdUser { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Correo Electrónico no es válido.")]
        public string email { get; set; } = string.Empty;
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d).{8,}$", ErrorMessage = "La contraseña debe tener minimo 8 caracteres")]
        public string passwordU { get; set; } = string.Empty;
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d).{8,}$", ErrorMessage = "La contraseña debe tener mayúscula, minúscula, un número y un caracter especial")]
        public string confirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string userName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string lastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Cédula es obligatorio.")]
        [RegularExpression(@"\d{9}", ErrorMessage = "La Cédula debe tener 9 dígitos y solo ser números")]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Teléfono es obligatorio.")]
        [RegularExpression(@"\d{8}", ErrorMessage = "El Teléfono debe tener 8 dígitos y solo ser números")]
        public string cellphone { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public int ptoDays { get; set; }
        public bool statusU { get; set; }
        public byte IdRol { get; set; }

    }
}
