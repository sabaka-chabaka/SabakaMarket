using SabakaMarket.CatalogService.Domain.Entities;

namespace SabakaMarket.CatalogService.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Product>> GetAllActiveAsync();
    Task<IReadOnlyList<Product>> GetBySellerIdAsync(Guid sellerId);
    Task CreateAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}