using System.ComponentModel.DataAnnotations;

namespace SRMA.Entities
{
    public class ProductEntity
    {

        public long IdProduct { get; set; }

        [Required(ErrorMessage = "El campo Nombre del Producto es obligatorio.")]
        public string productName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Cantidad es obligatorio.")]
        public int stock { get; set; }

        [Required(ErrorMessage = "El campo Detalles es obligatorio.")]
        public string details { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Estado del Producto es obligatorio.")]
        public string productStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        public int price { get; set; }

        [Required(ErrorMessage = "El campo ID del proveedor es obligatorio.")]
        public long IdSupplier { get; set; }





    }
}
