using Microsoft.AspNetCore.Mvc;
using ShopEase.Api.Models;
using ShopEase.Api.Services;

namespace ShopEase.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var items = await _cartService.GetCartItemsAsync(userId);
            return Ok(items);
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem item)
        {
            await _cartService.AddToCartAsync(item);
            return Ok();
        }

        // DELETE: api/cart/{cartItemId}
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);
            return NoContent();
        }
    }
}
