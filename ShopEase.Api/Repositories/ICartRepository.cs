using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task AddOrUpdateCartItemAsync(CartItem item);
        Task RemoveFromCartAsync(int cartItemId);
    }
}
