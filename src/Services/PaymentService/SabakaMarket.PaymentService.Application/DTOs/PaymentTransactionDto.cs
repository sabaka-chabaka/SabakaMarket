namespace SabakaMarket.PaymentService.Application.DTOs;

public record PaymentTransactionDto(
    Guid Id,
    Guid OrderId,
    Guid BuyerId,
    Guid SellerId,
    decimal Amount,
    string Status,
    DateTime CreatedAt,
    DateTime? UpdatedAt);