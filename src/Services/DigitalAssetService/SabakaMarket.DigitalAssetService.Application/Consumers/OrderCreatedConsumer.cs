using MassTransit;
using SabakaMarket.Contracts.Orders;
using SabakaMarket.Contracts.DigitalAssets;
using SabakaMarket.DigitalAssetService.Application.Services;

namespace SabakaMarket.DigitalAssetService.Application.Consumers;

public class OrderCreatedConsumer(
    IDigitalAssetService assetService, 
    IPublishEndpoint publishEndpoint) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var @event = context.Message;

        bool isReserved = await assetService.ReserveAssetForOrderAsync(@event.OrderId, @event.ProductId);

        if (isReserved)
        {
            await publishEndpoint.Publish(new AssetAllocatedEvent
            {
                OrderId = @event.OrderId,
                AllocatedAt = DateTime.UtcNow
            });
        }
        else
        {
            await publishEndpoint.Publish(new AssetAllocationFailedEvent
            {
                OrderId = @event.OrderId,
                Reason = "Нет доступных цифровых ключей для данного товара."
            });
        }
    }
}