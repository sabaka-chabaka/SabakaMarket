using SabakaMarket.PaymentService.Domain.Enums;

namespace SabakaMarket.PaymentService.Domain.Entities;

public class PaymentTransaction
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid BuyerId { get; private set; }
    public Guid SellerId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private PaymentTransaction() { }

    public PaymentTransaction(Guid id, Guid orderId, Guid buyerId, Guid sellerId, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Сумма платежа должна быть строго больше нуля.");

        if (buyerId == sellerId)
            throw new InvalidOperationException("Покупатель и продавец не могут быть одним лицом.");

        Id = id;
        OrderId = orderId;
        BuyerId = buyerId;
        SellerId = sellerId;
        Amount = amount;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Status != PaymentStatus.Pending)
            throw new InvalidOperationException($"Нельзя завершить платеж, находящийся в статусе {Status}.");

        Status = PaymentStatus.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        if (Status != PaymentStatus.Pending)
            throw new InvalidOperationException($"Нельзя перевести в статус Failed платеж из статуса {Status}.");

        Status = PaymentStatus.Failed;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void Refund()
    {
        if (Status != PaymentStatus.Completed)
            throw new InvalidOperationException("Возврат средств возможен только для успешно завершенных платежей.");

        Status = PaymentStatus.Refunded;
        UpdatedAt = DateTime.UtcNow;
    }
}