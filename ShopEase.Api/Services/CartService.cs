using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopEase.Api.Models;
using ShopEase.Api.Repositories;

namespace ShopEase.Api.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task AddProductAsync(Product product) => _cartRepository.AddProductAsync(product);

        public Task RemoveProductAsync(int productId) =>
            _cartRepository.RemoveProductAsync(productId);

        public Task<IEnumerable<Product>> GetCartItemsAsync() =>
            _cartRepository.GetCartItemsAsync();

        public decimal CalculateTotal(IEnumerable<Product> products)
        {
            return products.Sum(p => p.Price);
        }
    }
}
