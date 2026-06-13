namespace SabakaMarket.CatalogService.Application.DTOs;

public record CreateProductDto(string Name, string Description, decimal Price, int Quantity);