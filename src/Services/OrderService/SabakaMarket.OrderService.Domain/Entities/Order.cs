using SabakaMarket.OrderService.Domain.Enums;

namespace SabakaMarket.OrderService.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public Guid AssetId { get; private set; }
    public Guid SellerId { get; private set; }
    public Guid BuyerId { get; private set; }
    public decimal Price { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { }

    public Order(Guid id, Guid assetId, Guid sellerId, Guid buyerId, decimal price)
    {
        if (price <= 0)
            throw new ArgumentException("Цена заказа должна быть строго больше нуля.");
        
        if (sellerId == buyerId)
            throw new InvalidOperationException("Нельзя купить цифровой товар у самого себя.");

        Id = id;
        AssetId = assetId;
        SellerId = sellerId;
        BuyerId = buyerId;
        Price = price;
        Status = OrderStatus.Created;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException($"Нельзя оплатить заказ, находящийся в статусе {Status}.");

        Status = OrderStatus.Paid;
    }

    public void Complete()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException($"Нельзя завершить заказ до того, как он будет оплачен. Текущий статус: {Status}.");

        Status = OrderStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException("Нельзя отменить уже успешно завершенный и выданный заказ.");

        Status = OrderStatus.Cancelled;
    }
}