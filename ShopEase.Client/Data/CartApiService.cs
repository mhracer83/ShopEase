using System.Collections.Generic;
using System.Net;
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

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            var response = await _http.GetAsync("api/cart");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<CartItem>>();
        }

        public async Task AddToCartAsync(CartItem item) =>
            await _http.PostAsJsonAsync("api/cart/add", item);

        public async Task RemoveFromCartAsync(int cartItemId) =>
            await _http.DeleteAsync($"api/cart/{cartItemId}");
    }
}
