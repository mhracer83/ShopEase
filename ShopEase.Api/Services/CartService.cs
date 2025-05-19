using System.Collections.Generic;
using System.Threading.Tasks;
using ShopEase.Api.Models;
using ShopEase.Api.Repositories;

namespace ShopEase.Api.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;

        public CartService(ICartRepository repo)
        {
            _repo = repo;
        }

        public Task<List<CartItem>> GetCartItemsAsync(int userId) =>
            _repo.GetCartItemsAsync(userId);

        public Task AddToCartAsync(CartItem item) => _repo.AddOrUpdateCartItemAsync(item);

        public Task RemoveFromCartAsync(int cartItemId, int userId) =>
            _repo.RemoveFromCartAsync(cartItemId, userId);
    }
}
