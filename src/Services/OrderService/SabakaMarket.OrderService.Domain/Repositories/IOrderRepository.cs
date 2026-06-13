using SabakaMarket.OrderService.Domain.Entities;

namespace SabakaMarket.OrderService.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Order>> GetByBuyerIdAsync(Guid buyerId);
    Task<IReadOnlyList<Order>> GetBySellerIdAsync(Guid sellerId);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}