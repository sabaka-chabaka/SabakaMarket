namespace SabakaMarket.CatalogService.Application.DTOs;

public record ProductDto(Guid Id, string Name, string Description, decimal Price, int Quantity, DateTime CreatedAt, bool IsActive);