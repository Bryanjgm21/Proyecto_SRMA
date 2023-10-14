namespace SRMA.Entities
{
    public class ProductEntity
    {
        public long IdProduct { get; set; }
        public string productName { get; set; } = string.Empty;
        public string details { get; set; } = string.Empty;
        public string stock { get; set; } = string.Empty;
        public string productStatus { get; set; } = string.Empty;
        public string price { get; set; } = string.Empty;
        public string IdSupplier { get; set; } = string.Empty;
    }
}
