namespace SabakaMarket.Contracts.Orders;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }
    public Guid BuyerId { get; set; }
    public Guid SellerId { get; set; }
    public Guid ProductId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}