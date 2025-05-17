using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ShopEase.Client.Data
{
    public class CartApiService : ICartApiService
    {
        private readonly HttpClient _http;

        public CartApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int userId) =>
            await _http.GetFromJsonAsync<List<CartItem>>($"api/cart/{userId}");

        public async Task AddToCartAsync(CartItem item) =>
            await _http.PostAsJsonAsync("api/cart/add", item);

        public async Task RemoveFromCartAsync(int cartItemId) =>
            await _http.DeleteAsync($"api/cart/{cartItemId}");
    }
}
