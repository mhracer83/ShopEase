using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Services
{
    public interface ICartService
    {
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(int productId);
        Task<IEnumerable<Product>> GetCartItemsAsync();
        decimal CalculateTotal(IEnumerable<Product> products);
    }
}
