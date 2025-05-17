using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopEase.Client.Data
{
    public interface ICartApiService
    {
        Task<List<CartItem>> GetCartItemsAsync(int userId);
        Task AddToCartAsync(CartItem item);
        Task RemoveFromCartAsync(int cartItemId);
    }
}
