using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopEase.Client.Data
{
    public interface IProductApiService
    {
        Task<List<Product>> GetProductCatalogAsync();
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(int productId);
    }
}
