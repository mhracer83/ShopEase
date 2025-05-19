using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopEase.Api.Models;
using ShopEase.Api.Services;

namespace ShopEase.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            var items = await _cartService.GetCartItemsAsync(userId.Value);
            return Ok(items);
        }

        // POST: api/cart/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem item)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            // Overwrite any userId from client with the one from claims
            item.UserID = userId.Value;

            await _cartService.AddToCartAsync(item);
            return Ok();
        }

        // DELETE: api/cart/{cartItemId}
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = GetUserId();
            if (userId == null)
                return Unauthorized();

            await _cartService.RemoveFromCartAsync(cartItemId, userId.Value);
            return NoContent();
        }

        // Helper to extract userId from JWT claims
        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int id) ? id : null;
        }
    }
}
