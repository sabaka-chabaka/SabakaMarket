using Microsoft.EntityFrameworkCore;
using SabakaMarket.PaymentService.Domain.Entities;
using SabakaMarket.PaymentService.Domain.Repositories;
using SabakaMarket.PaymentService.Infrastructure.Data;

namespace SabakaMarket.PaymentService.Infrastructure.Repositories;

public class PaymentRepository(PaymentDbContext dbContext) : IPaymentRepository
{
    public async Task<PaymentTransaction?> GetByIdAsync(Guid id) =>
        await dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<PaymentTransaction?> GetByOrderIdAsync(Guid orderId) =>
        await dbContext.Transactions.FirstOrDefaultAsync(t => t.OrderId == orderId);

    public async Task<IReadOnlyList<PaymentTransaction>> GetByBuyerIdAsync(Guid buyerId) =>
        await dbContext.Transactions.Where(t => t.BuyerId == buyerId).ToListAsync();

    public async Task AddAsync(PaymentTransaction transaction) =>
        await dbContext.Transactions.AddAsync(transaction);

    public async Task UpdateAsync(PaymentTransaction transaction) =>
        dbContext.Transactions.Update(transaction);
}