using SabakaMarket.CatalogService.Application.DTOs;

namespace SabakaMarket.CatalogService.Application.Services;

public interface ICatalogService
{
    Task<IReadOnlyList<ProductDto>> GetActiveProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(Guid id);

    Task<ProductDto> CreateProductAsync(Guid sellerId, CreateProductDto product);
    
    Task<bool> ReserveStockAsync(Guid productId, int quantity);
    Task<bool> ReleaseStockAsync(Guid productId, int quantity);
}