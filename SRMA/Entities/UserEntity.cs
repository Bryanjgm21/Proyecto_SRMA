﻿using System.ComponentModel.DataAnnotations;

namespace SRMA.Entities
{
    public class UserEntity
    {

        public long IdUser { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Correo inválido")]
        public string email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+{}|':;<>,.?~\\-]).{8,}$", ErrorMessage = "La contraseña debe tener minimo 8 caracteres, tener mayúscula, minúscula, un número y un caracter especial.")]
        public string? passwordU { get; set; } = string.Empty;

        [RegularExpression("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+{}|':;<>,.?~\\-]).{8,}$", ErrorMessage = "La contraseña debe tener minimo 8 caracteres, tener mayúscula, minúscula, un número y un caracter especial.")]
        
        public string? confirmPassword { get; set; } = string.Empty;

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
        public bool statusU { get; set; }
        public byte? IdRol { get; set; }

        public string? tempPassword { get; set; } = string.Empty;
        public bool? tempKey { get; set; }

        public string? IdUserEncrypt { get; set; } = string.Empty;

        //FK FidelityProgram
         public long? IdProgram { get; set; }

        //FK InfoEm

        public long IdE { get; set; }

        [Required(ErrorMessage = "El campo salario es obligatorio.")]
        [RegularExpression(@"\d{4,6}", ErrorMessage = "El salario debe ser mayor a 10,000 y menor a un millón")]
        public int? salary { get; set; }
        [Required(ErrorMessage = "El campo Puesto es obligatorio.")]
        public string job { get; set; } = string.Empty;
        [Required(ErrorMessage = "El campo Horario es obligatorio.")]
        public string scheduleE { get; set; } = string.Empty;
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "El campo Dias vacaciones es obligatorio.")]
        [RegularExpression(@"^[1-9]\d{0,1}$", ErrorMessage = "Debe solo ser solo números")]
        [Range(1, 15, ErrorMessage = "El valor debe estar entre 1 y 12.")]
        public int? ptoDays { get; set; }


    }
}
