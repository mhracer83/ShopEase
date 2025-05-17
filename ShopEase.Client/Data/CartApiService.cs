using System;
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

        public async Task<List<Product>> GetCartItemsAsync() =>
            await _http.GetFromJsonAsync<List<Product>>("api/cart");

        public async Task AddProductAsync(Product product) =>
            await _http.PostAsJsonAsync("api/cart", product);

        public async Task RemoveProductAsync(int productId) =>
            await _http.DeleteAsync($"api/cart/{productId}");
    }
}
