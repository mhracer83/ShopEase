using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;

namespace ShopEase.Api.Services
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task AddToCartAsync(CartItem item);
        Task RemoveFromCartAsync(int cartItemId, int userId);
    }
}
