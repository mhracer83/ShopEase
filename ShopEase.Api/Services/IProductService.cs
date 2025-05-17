using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Services
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(int productId);
        Task<IEnumerable<Product>> GetProductCatalogAsync();
        decimal CalculateTotal(IEnumerable<Product> products);
    }
}
