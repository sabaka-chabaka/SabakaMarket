using SabakaMarket.DigitalAssetService.Domain.Enums;

namespace SabakaMarket.DigitalAssetService.Domain.Entities;

public class DigitalAsset
{
    public Guid Id { get; set; }
    public Guid ProductId { get; private set; }
    public Guid SellerId { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public AssetStatus Status { get; private set; }
    public Guid? OrderId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private DigitalAsset() { }
    
    public DigitalAsset(Guid id, Guid productId, Guid sellerId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be null or whitespace.");

        Id = id;
        ProductId = productId;
        SellerId = sellerId;
        Content = content;
        Status = AssetStatus.Available;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void Reserve(Guid orderId)
    {
        if (Status != AssetStatus.Available)
            throw new InvalidOperationException($"Нельзя зарезервировать товар из статуса {Status}.");

        Status = AssetStatus.Reserved;
        OrderId = orderId;
    }

    public void ConfirmSale()
    {
        if (Status != AssetStatus.Reserved)
            throw new InvalidOperationException($"Нельзя продать не зарезервированный товар. Текущий статус: {Status}.");

        Status = AssetStatus.Sold;
    }

    public void ReleaseReservation()
    {
        if (Status != AssetStatus.Reserved)
            throw new InvalidOperationException($"Нельзя снять резерв с товара в статусе {Status}.");

        Status = AssetStatus.Available;
        OrderId = null;
    }
}