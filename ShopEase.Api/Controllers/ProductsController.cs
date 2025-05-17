using Microsoft.AspNetCore.Mvc;
using ShopEase.Api.Models;
using ShopEase.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly IProductService _productService;

    public CartController(IProductService cartService) => _productService = cartService;

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _productService.GetProductCatalogAsync());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        await _productService.AddProductAsync(product);
        return CreatedAtAction(nameof(Get), null, product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.RemoveProductAsync(id);
        return NoContent();
    }
}
