using Microsoft.EntityFrameworkCore;
using SabakaMarket.DigitalAssetService.Domain.Entities;
using SabakaMarket.DigitalAssetService.Domain.Enums;
using SabakaMarket.DigitalAssetService.Domain.Repositories;
using SabakaMarket.DigitalAssetService.Infrastructure.Data;

namespace SabakaMarket.DigitalAssetService.Infrastructure.Repositories;

public class DigitalAssetRepository(DigitalAssetDbContext dbContext) : IDigitalAssetRepository
{
    public async Task<DigitalAsset?> GetByIdAsync(Guid id) =>
        await dbContext.DigitalAssets.FirstOrDefaultAsync(da => da.Id == id);
    
    public async Task<DigitalAsset?> GetByOrderIdAsync(Guid orderId) => 
        await dbContext.DigitalAssets.FirstOrDefaultAsync(da => da.OrderId == orderId);

    public async Task<DigitalAsset?> GetAvailableByProductIdAsync(Guid productId) =>
        await dbContext.DigitalAssets
            .Where(a => a.ProductId == productId && a.Status == AssetStatus.Available)
            .FirstOrDefaultAsync();

    public async Task<IReadOnlyList<DigitalAsset>> GetBySellerIdAsync(Guid sellerId) => 
        await dbContext.DigitalAssets.Where(da => da.SellerId == sellerId).ToListAsync();

    public async Task AddAsync(DigitalAsset asset) =>
        await dbContext.DigitalAssets.AddAsync(asset);

    public async Task UpdateAsync(DigitalAsset asset) =>
        dbContext.DigitalAssets.Update(asset);
}