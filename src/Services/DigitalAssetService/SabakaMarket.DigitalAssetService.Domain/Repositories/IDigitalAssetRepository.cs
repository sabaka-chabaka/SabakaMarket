using SabakaMarket.DigitalAssetService.Domain.Entities;

namespace SabakaMarket.DigitalAssetService.Domain.Repositories;

public interface IDigitalAssetRepository
{
    Task<DigitalAsset?> GetByIdAsync(Guid id);
    Task<DigitalAsset?> GetByOrderIdAsync(Guid orderId);
    
    Task<DigitalAsset?> GetByAvailableProductIdAsync(Guid productId);
    
    Task<IReadOnlyList<DigitalAsset>> GetBySellerIdAsync(Guid sellerId);
    Task AddAsync(DigitalAsset digitalAsset);
    Task UpdateAsync(DigitalAsset digitalAsset);
}