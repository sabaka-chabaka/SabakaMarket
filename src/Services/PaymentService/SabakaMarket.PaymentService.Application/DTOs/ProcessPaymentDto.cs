namespace SabakaMarket.PaymentService.Application.DTOs;

public record ProcessPaymentDto(Guid OrderId, Guid SellerId, decimal Amount);