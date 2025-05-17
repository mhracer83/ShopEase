using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopEase.Client.Data
{
    public interface ICartApiService
    {
        Task<List<Product>> GetCartItemsAsync();
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(int productId);
    }
}
