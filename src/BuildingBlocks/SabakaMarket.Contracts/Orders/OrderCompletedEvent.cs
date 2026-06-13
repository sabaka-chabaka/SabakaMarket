namespace SabakaMarket.Contracts.Orders;

public class OrderCompletedEvent
{
    public Guid OrderId { get; set; }
    public Guid SellerId { get; set; }
    public Guid BuyerId { get; set; }
    public decimal Amount { get; set; }
}