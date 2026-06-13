using Microsoft.EntityFrameworkCore;
using SabakaMarket.OrderService.Domain.Entities;
using SabakaMarket.OrderService.Domain.Repositories;
using SabakaMarket.OrderService.Infrastructure.Data;

namespace SabakaMarket.OrderService.Infrastructure.Repositories;

public class OrderRepository(OrderDbContext dbContext) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id) => 
        await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

    public async Task<IReadOnlyList<Order>> GetByBuyerIdAsync(Guid buyerId) =>
        await dbContext.Orders.Where(o => o.BuyerId == buyerId).ToListAsync();

    public async Task<IReadOnlyList<Order>> GetBySellerIdAsync(Guid sellerId) =>
        await dbContext.Orders.Where(o => o.SellerId == sellerId).ToListAsync();

    public async Task AddAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync();
    }
}