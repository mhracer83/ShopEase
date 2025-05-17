using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(int productId);
        Task<IEnumerable<Product>> GetProductCatalogAsync();
    }
}
