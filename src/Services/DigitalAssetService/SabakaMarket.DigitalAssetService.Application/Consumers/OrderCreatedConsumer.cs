using MassTransit;
using SabakaMarket.Contracts.Orders;
using SabakaMarket.DigitalAssetService.Application.Services;

namespace SabakaMarket.DigitalAssetService.Application.Consumers;

public class OrderCreatedConsumer(IDigitalAssetService assetService) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var @event = context.Message;
        
        bool isReserved = await assetService.ReserveAssetForOrderAsync(@event.OrderId, @event.ProductId);

        if (!isReserved)
        {
            //TODO: OrderReservationFailedEvent
            throw new InvalidOperationException($"No available asset for order {context.Message.OrderId}.");
        }
    }
}