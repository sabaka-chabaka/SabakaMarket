using SabakaMarket.PaymentService.Application.DTOs;

namespace SabakaMarket.PaymentService.Application.Services;

public interface IPaymentService
{
    Task<PaymentTransactionDto> InitializePaymentAsync(Guid buyerId, ProcessPaymentDto dto);
    
    Task<bool> CompletePaymentAsync(Guid orderId);
    
    Task<bool> FailPaymentAsync(Guid orderId);
    
    Task<bool> RefundPaymentAsync(Guid orderId);
    
    Task<PaymentTransactionDto?> GetByOrderIdAsync(Guid orderId);
    Task<IReadOnlyList<PaymentTransactionDto>> GetMyTransactionsAsync(Guid buyerId);
}