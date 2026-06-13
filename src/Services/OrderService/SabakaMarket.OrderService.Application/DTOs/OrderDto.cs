namespace SabakaMarket.OrderService.Application.DTOs;

public record OrderDto(
    Guid Id,
    Guid AssetId,
    Guid SellerId,
    Guid BuyerId,
    decimal Price,
    string Status,
    DateTime CreatedAt);