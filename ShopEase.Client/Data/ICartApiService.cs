using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopEase.Client.Data
{
    public interface ICartApiService
    {
        Task<List<CartItem>> GetCartItemsAsync();
        Task AddToCartAsync(CartItem item);
        Task RemoveFromCartAsync(int cartItemId);
    }
}
