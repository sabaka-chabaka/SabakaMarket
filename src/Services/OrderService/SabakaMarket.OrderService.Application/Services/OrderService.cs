using MassTransit;
using SabakaMarket.Contracts.Orders;
using SabakaMarket.OrderService.Application.DTOs;
using SabakaMarket.OrderService.Domain.Entities;
using SabakaMarket.OrderService.Domain.Repositories;

namespace SabakaMarket.OrderService.Application.Services;

public class OrderService(
    IOrderRepository orders, 
    IPublishEndpoint publishEndpoint) : IOrderService
{
    public async Task<OrderDto> CreateOrderAsync(Guid buyerId, CreateOrderDto dto)
    {
        var order = new Order(Guid.NewGuid(), dto.AssetId, dto.SellerId, buyerId, dto.Price);
        
        await orders.AddAsync(order);

        await publishEndpoint.Publish(new OrderCreatedEvent
        {
            OrderId = order.Id,
            BuyerId = order.BuyerId,
            SellerId = order.SellerId,
            ProductId = order.AssetId,
            TotalPrice = order.Price,
            CreatedAt = order.CreatedAt
        });
        
        return MapToDto(order);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(Guid id)
    {
        var order = await orders.GetByIdAsync(id);
        if (order is null) return null;
        
        return MapToDto(order);
    }

    public async Task<IReadOnlyList<OrderDto>> GetOrdersByBuyerAsync(Guid buyerId)
    {
        var ords = await orders.GetByBuyerIdAsync(buyerId);
        return ords.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<OrderDto>> GetOrdersBySellerAsync(Guid sellerId)
    {
        var ords = await orders.GetBySellerIdAsync(sellerId);
        return ords.Select(MapToDto).ToList();
    }

    public async Task<bool> PayOrderAsync(Guid id)
    {
        var order = await orders.GetByIdAsync(id);
        if (order is null) return false;

        order.MarkAsPaid();
        await orders.UpdateAsync(order);

        await publishEndpoint.Publish(new OrderCompletedEvent { OrderId = order.Id, SellerId = order.SellerId, BuyerId = order.BuyerId, Amount = order.Price });

    return true;
}

    public async Task<bool> CancelOrderAsync(Guid id)
    {
        var order = await orders.GetByIdAsync(id);
        if (order is null) return false;

        order.Cancel();
        await orders.UpdateAsync(order);

        return true;
    }

    private static OrderDto MapToDto(Order order) =>
        new OrderDto(
            order.Id, 
            order.AssetId, 
            order.SellerId, 
            order.BuyerId, 
            order.Price, 
            order.Status.ToString(), 
            order.CreatedAt);
}