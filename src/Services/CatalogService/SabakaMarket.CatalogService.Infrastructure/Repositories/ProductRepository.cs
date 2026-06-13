using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SabakaMarket.CatalogService.Domain.Entities;
using SabakaMarket.CatalogService.Domain.Repositories;
using SabakaMarket.CatalogService.Infrastructure.Settings;

namespace SabakaMarket.CatalogService.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IOptions<MongoSettings> mongoSettings)
    {
        var client = new MongoClient(mongoSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoSettings.Value.DatabaseName);
        
        _products = database.GetCollection<Product>(nameof(Product));
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<IReadOnlyList<Product>> GetAllActiveAsync()
    {
        return await _products.Find(p => p.IsActive).ToListAsync();
    }
    
    public async Task<IReadOnlyList<Product>> GetBySellerIdAsync(Guid sellerId)
    {
        return await _products.Find(p => p.SellerId == sellerId).ToListAsync();
    }

    public async Task CreateAsync(Product product)
    {
        await _products.InsertOneAsync(product);
    }
    
    public async Task UpdateAsync(Product product)
    {
        await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _products.DeleteOneAsync(p => p.Id == id);
    }   
}