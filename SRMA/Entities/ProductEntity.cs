using System.ComponentModel.DataAnnotations;

namespace SRMA.Entities
{
    public class ProductEntity
    {
        public long IdProduct { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Producto es obligatorio.")]
        public string productName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        [RegularExpression(@"^[1-9]\d{0,2}$", ErrorMessage = "Debe solo ser solo números")]
        public int stock { get; set; }

        [Required(ErrorMessage = "El campo Detalles es obligatorio.")]
        public string details { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Estado del Producto es obligatorio.")]
        public string productStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [RegularExpression(@"^[1-9]\d{0,15}$", ErrorMessage = "Debe solo ser solo números")]
        public int price { get; set; }

        public long IdSupplier { get; set; }
        public bool productCond { get; set; }

        public string supplierName { get; set; } = string.Empty;


    }
}
