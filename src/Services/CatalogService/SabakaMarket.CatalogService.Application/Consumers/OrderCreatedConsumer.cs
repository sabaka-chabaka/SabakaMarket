using MassTransit;
using SabakaMarket.Contracts.Orders;
using SabakaMarket.CatalogService.Application.Services;

namespace SabakaMarket.CatalogService.Application.Consumers;

public class OrderCreatedConsumer(ICatalogService catalogService) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        bool isReserved = await catalogService.ReserveStockAsync(message.ProductId, 1);

        if (!isReserved)
        {
            throw new InvalidOperationException($"Не удалось зарезервировать товар {message.ProductId} для заказа {message.OrderId}. Нет в наличии.");
        }
    }
}