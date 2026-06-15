namespace SabakaMarket.DigitalAssetService.Application.DTOs;

public record DigitalAssetDto(Guid Id, Guid ProductId, Guid SellerId, string Content, string Status, Guid? OrderId, DateTime CreatedAt);