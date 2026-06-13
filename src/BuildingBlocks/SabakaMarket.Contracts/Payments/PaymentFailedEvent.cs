namespace SabakaMarket.Contracts.Payments;

public class PaymentFailedEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime FailedAt { get; set; }
}