namespace SabakaMarket.Contracts.DigitalAssets;

public class AssetAllocatedEvent
{
    public Guid OrderId { get; set; }
    public string CdKey { get; set; } = string.Empty;
    public DateTime AllocatedAt { get; set; }
}