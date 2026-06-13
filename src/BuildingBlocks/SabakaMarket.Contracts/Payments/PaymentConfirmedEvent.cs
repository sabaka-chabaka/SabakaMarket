namespace SabakaMarket.Contracts.Payments;

public class PaymentConfirmedEvent
{
    public Guid OrderId { get; set; }
    public Guid BuyerId { get; set; }   
    public Guid SellerId { get; set; }  
    public decimal Amount { get; set; } 
    public DateTime CreatedAt { get; set; }
}