using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopEase.Api.Models;
using ShopEase.Api.Services;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) => _productService = productService;

    [AllowAnonymous]
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
