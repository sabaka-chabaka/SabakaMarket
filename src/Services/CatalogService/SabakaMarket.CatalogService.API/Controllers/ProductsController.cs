using Microsoft.AspNetCore.Mvc;
using SabakaMarket.CatalogService.Application.DTOs;
using SabakaMarket.CatalogService.Application.Services;

namespace SabakaMarket.CatalogService.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(ICatalogService catalogService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetActive() => Ok(await catalogService.GetActiveProductsAsync()); 
    

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var product = await catalogService.GetProductByIdAsync(id);
        
        if (product is null)
        {
            return NotFound(new { Message = $"Товар с ID {id} не найден." });
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto dto)
    {
        var nameIdentifier = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                             ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(nameIdentifier) || !Guid.TryParse(nameIdentifier, out var sellerId))
        {
            return Unauthorized(new { Message = "Не удалось определить пользователя из токена." });
        }

        var result = await catalogService.CreateProductAsync(sellerId, dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
