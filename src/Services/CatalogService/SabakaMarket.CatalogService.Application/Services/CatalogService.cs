using SabakaMarket.CatalogService.Application.DTOs;
using SabakaMarket.CatalogService.Domain.Entities;
using SabakaMarket.CatalogService.Domain.Repositories;

namespace SabakaMarket.CatalogService.Application.Services;

public class CatalogService(IProductRepository products) : ICatalogService
{
    public async Task<IReadOnlyList<ProductDto>> GetActiveProductsAsync()
    {
        var active = await products.GetAllActiveAsync();

        return active.Select(product => new ProductDto(product.Id, product.Name, product.Description, product.Price, product.Quantity, product.CreatedAt, product.IsActive)).ToList();
    }

    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var product = await products.GetByIdAsync(id);
        
        return product == null ? null : new ProductDto(product.Id, product.Name, product.Description, product.Price, product.Quantity, product.CreatedAt, product.IsActive);
    }

    public async Task<ProductDto> CreateProductAsync(Guid sellerId, CreateProductDto product)
    {
        var prodEntity = new Product(Guid.NewGuid(), sellerId, product.Name, product.Description, product.Price, product.Quantity);
        
        await products.CreateAsync(prodEntity);

        return new ProductDto(prodEntity.Id, prodEntity.Name, prodEntity.Description, prodEntity.Price,
            prodEntity.Quantity, prodEntity.CreatedAt, prodEntity.IsActive);
    }

    public async Task<bool> ReserveStockAsync(Guid productId, int quantity)
    {
        var product = await products.GetByIdAsync(productId);
        
        if (product == null || product.Quantity < quantity || !product.IsActive) return false;

        product.DecreaseQuantity(quantity);
        await products.UpdateAsync(product);
        
        return true;
    }

    public async Task<bool> ReleaseStockAsync(Guid productId, int quantity)
    {
        var product = await products.GetByIdAsync(productId);

        if (product == null) return false;
        product.UpdateQuantity(product.Quantity + quantity);
        
        await products.UpdateAsync(product);
        
        return true;
    }
}