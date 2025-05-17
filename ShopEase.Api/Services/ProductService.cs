using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopEase.Api.Models;
using ShopEase.Api.Repositories;

namespace ShopEase.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task AddProductAsync(Product product) => _productRepository.AddProductAsync(product);

        public Task RemoveProductAsync(int productId) =>
            _productRepository.RemoveProductAsync(productId);

        public Task<IEnumerable<Product>> GetProductCatalogAsync() =>
            _productRepository.GetProductCatalogAsync();

        public decimal CalculateTotal(IEnumerable<Product> products)
        {
            return products.Sum(p => p.Price);
        }
    }
}
