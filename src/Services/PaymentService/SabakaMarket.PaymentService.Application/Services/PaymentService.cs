using MassTransit;
using SabakaMarket.Contracts.Payments;
using SabakaMarket.PaymentService.Application.DTOs;
using SabakaMarket.PaymentService.Domain.Entities;
using SabakaMarket.PaymentService.Domain.Repositories;

namespace SabakaMarket.PaymentService.Application.Services;

public class PaymentService(
    IPaymentRepository paymentRepository,
    IPublishEndpoint publishEndpoint) : IPaymentService
{
    public async Task<PaymentTransactionDto> InitializePaymentAsync(Guid buyerId, ProcessPaymentDto dto)
    {
        var existingTx = await paymentRepository.GetByOrderIdAsync(dto.OrderId);
        if (existingTx is not null)
        {
            return MapToDto(existingTx);
        }

        var transaction = new PaymentTransaction(
            id: Guid.NewGuid(),
            orderId: dto.OrderId,
            buyerId: buyerId,
            sellerId: dto.SellerId,
            amount: dto.Amount
        );

        await paymentRepository.AddAsync(transaction);
        
        return MapToDto(transaction);
    }

    public async Task<bool> CompletePaymentAsync(Guid orderId)
    {
        var transaction = await paymentRepository.GetByOrderIdAsync(orderId);
        if (transaction is null || transaction.Status != Domain.Enums.PaymentStatus.Pending) 
            return false;

        transaction.Complete();
        await paymentRepository.UpdateAsync(transaction);

        await publishEndpoint.Publish(new PaymentConfirmedEvent
        {
            OrderId = transaction.OrderId,
            BuyerId = transaction.BuyerId,
            SellerId = transaction.SellerId,
            Amount = transaction.Amount,
            CreatedAt = DateTime.UtcNow
        });

        return true;
    }

    public async Task<bool> FailPaymentAsync(Guid orderId)
    {
        var transaction = await paymentRepository.GetByOrderIdAsync(orderId);
        if (transaction is null || transaction.Status != Domain.Enums.PaymentStatus.Pending) 
            return false;

        transaction.Fail();
        await paymentRepository.UpdateAsync(transaction);

        await publishEndpoint.Publish(new PaymentFailedEvent
        {
            OrderId = transaction.OrderId,
            Reason = "Недостаточно средств на балансе пользователя или отклонено платежным шлюзом.",
            FailedAt = DateTime.UtcNow
        });

        return true;
    }

    public async Task<bool> RefundPaymentAsync(Guid orderId)
    {
        var transaction = await paymentRepository.GetByOrderIdAsync(orderId);
        if (transaction is null || transaction.Status != Domain.Enums.PaymentStatus.Completed) 
            return false;

        transaction.Refund();
        await paymentRepository.UpdateAsync(transaction);

        return true;
    }

    public async Task<PaymentTransactionDto?> GetByOrderIdAsync(Guid orderId)
    {
        var transaction = await paymentRepository.GetByOrderIdAsync(orderId);
        return transaction is null ? null : MapToDto(transaction);
    }

    public async Task<IReadOnlyList<PaymentTransactionDto>> GetMyTransactionsAsync(Guid buyerId)
    {
        var txs = await paymentRepository.GetByBuyerIdAsync(buyerId);
        return txs.Select(MapToDto).ToList();
    }

    private static PaymentTransactionDto MapToDto(PaymentTransaction tx) =>
        new PaymentTransactionDto(
            tx.Id,
            tx.OrderId,
            tx.BuyerId,
            tx.SellerId,
            tx.Amount,
            tx.Status.ToString(),
            tx.CreatedAt,
            tx.UpdatedAt
        );
}