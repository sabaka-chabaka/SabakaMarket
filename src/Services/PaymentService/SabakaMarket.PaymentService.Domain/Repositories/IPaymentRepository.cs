using SabakaMarket.PaymentService.Domain.Entities;

namespace SabakaMarket.PaymentService.Domain.Repositories;

public interface IPaymentRepository
{
    Task<PaymentTransaction?> GetByIdAsync(Guid id);
    Task<PaymentTransaction?> GetByOrderIdAsync(Guid orderId);
    Task<IReadOnlyList<PaymentTransaction>> GetByBuyerIdAsync(Guid buyerId);
    Task AddAsync(PaymentTransaction transaction);
    Task UpdateAsync(PaymentTransaction transaction);
}