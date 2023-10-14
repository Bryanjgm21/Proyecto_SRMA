using SRMA.Interfaces;
namespace SRMA.Models
{
    public class ProductModel : IProductModel
    {
        private readonly IConfiguration _configuration;
        public ProductModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
    }
}
