using SabakaMarket.OrderService.Application.DTOs;

namespace SabakaMarket.OrderService.Application.Services;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(Guid buyerId, CreateOrderDto dto);
    Task<OrderDto?> GetOrderByIdAsync(Guid id);
    Task<IReadOnlyList<OrderDto>> GetOrdersByBuyerAsync(Guid buyerId);
    Task<IReadOnlyList<OrderDto>> GetOrdersBySellerAsync(Guid sellerId);
    Task<bool> PayOrderAsync(Guid id);
    Task<bool> CancelOrderAsync(Guid id);
}
