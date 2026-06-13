namespace SabakaMarket.OrderService.Application.DTOs;

public record CreateOrderDto(Guid AssetId, Guid SellerId, decimal Price);