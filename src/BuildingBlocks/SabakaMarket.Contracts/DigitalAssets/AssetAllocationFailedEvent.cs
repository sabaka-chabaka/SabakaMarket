namespace SabakaMarket.Contracts.DigitalAssets;

public record AssetAllocationFailedEvent
{
    public Guid OrderId { get; init; }
    public string Reason { get; init; } = string.Empty;
    public DateTime FailedAt { get; init; }
}