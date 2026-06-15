using SabakaMarket.DigitalAssetService.Application.DTOs;
using SabakaMarket.DigitalAssetService.Domain.Entities;
using SabakaMarket.DigitalAssetService.Domain.Repositories;

namespace SabakaMarket.DigitalAssetService.Application.Services;

public class DigitalAssetService(IDigitalAssetRepository digitalAssets) : IDigitalAssetService
{
    public async Task<DigitalAssetDto> UploadAssetAsync(Guid sellerId, UploadAssetDto dto)
    {
        var digitalAsset = new DigitalAsset(Guid.NewGuid(), dto.ProductId, sellerId, dto.Content);

        await digitalAssets.AddAsync(digitalAsset);
        
        return new DigitalAssetDto(digitalAsset.Id, digitalAsset.ProductId, digitalAsset.SellerId, digitalAsset.Content, digitalAsset.Status.ToString(), digitalAsset.OrderId, digitalAsset.CreatedAt);
    }

    public async Task<IReadOnlyList<DigitalAssetDto>> GetMyAssetsAsync(Guid sellerId)
    {
        var assets = await digitalAssets.GetBySellerIdAsync(sellerId);
        return assets.Select(asset => new DigitalAssetDto(asset.Id, asset.ProductId, asset.SellerId, asset.Content, asset.Status.ToString(), asset.OrderId, asset.CreatedAt)).ToList();
    }

    public async Task<bool> ReserveAssetForOrderAsync(Guid orderId, Guid productId)
    {
        var asset = await digitalAssets.GetAvailableByProductIdAsync(productId);
        if (asset is null) return false;
        asset.Reserve(orderId);
        await digitalAssets.UpdateAsync(asset);
        
        return true;
    }

    public async Task<bool> ConfirmSaleForOrderAsync(Guid orderId)
    {
        var asset = await digitalAssets.GetByOrderIdAsync(orderId);
        if (asset is null) return false;
        asset.ConfirmSale();
        await digitalAssets.UpdateAsync(asset);
        return true;
    }

    public async Task<bool> ReleaseReservationForOrderAsync(Guid orderId)
    {
        var asset = await digitalAssets.GetByOrderIdAsync(orderId);
        if (asset is null) return false;
        asset.ReleaseReservation();
        await digitalAssets.UpdateAsync(asset);
        return true;
    }
}