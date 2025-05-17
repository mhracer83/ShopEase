using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Repositories
{
    public interface ICartRepository
    {
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(int productId);
        Task<IEnumerable<Product>> GetCartItemsAsync();
    }
}
