namespace SRMA.Entities
{
    public class ProductEntity
    {
        public long IdProduct { get; set; }
        public string productName { get; set; } = string.Empty;
        public string details { get; set; } = string.Empty;
        public int stock { get; set; }
        public string productStatus { get; set; } = string.Empty;
        public int price { get; set; }
        public long IdSupplier { get; set; }
    }
}
