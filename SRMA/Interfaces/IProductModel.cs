using SRMA.Entities;

namespace SRMA.Interfaces
{
    public interface IProductModel
    {
        public List<ProductEntity> ListProducts();
        public ProductEntity? GetProductById(long IdProduct);
        public ProductEntity? InsertProduct(ProductEntity entity);
        public ProductEntity? UpdateProduct(ProductEntity entity, long IdProduct);
        public ProductEntity? DeleteProduct(long IdProduct);
    }
}
