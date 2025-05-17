using Microsoft.AspNetCore.Mvc;
using ShopEase.Api.Models; // Add this line if Product is in this namespace
using ShopEase.Api.Services; // Add this line if ICartService is in this namespace

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService) => _cartService = cartService;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _cartService.GetCartItemsAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        await _cartService.AddProductAsync(product);
        return CreatedAtAction(nameof(Get), null, product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _cartService.RemoveProductAsync(id);
        return NoContent();
    }
}
