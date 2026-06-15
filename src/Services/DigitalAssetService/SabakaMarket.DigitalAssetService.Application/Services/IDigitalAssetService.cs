using SabakaMarket.DigitalAssetService.Application.DTOs;

namespace SabakaMarket.DigitalAssetService.Application.Services;

public interface IDigitalAssetService
{
    Task<DigitalAssetDto> UploadAssetAsync(Guid sellerId, UploadAssetDto dto);
    Task<IReadOnlyList<DigitalAssetDto>> GetMyAssetsAsync(Guid sellerId);
    
    Task<bool> ReserveAssetForOrderAsync(Guid orderId, Guid productId);
    Task<bool> ConfirmSaleForOrderAsync(Guid orderId);
    Task<bool> ReleaseReservationForOrderAsync(Guid orderId);
}