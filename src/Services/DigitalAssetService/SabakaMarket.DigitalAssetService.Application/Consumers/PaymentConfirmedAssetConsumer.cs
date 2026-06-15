using MassTransit;
using SabakaMarket.Contracts.Payments;
using SabakaMarket.DigitalAssetService.Application.Services;

namespace SabakaMarket.DigitalAssetService.Application.Consumers;

public class PaymentConfirmedAssetConsumer(IDigitalAssetService assetService) : IConsumer<PaymentConfirmedEvent>
{
    public async Task Consume(ConsumeContext<PaymentConfirmedEvent> context)
    {
        var @event = context.Message;

        await assetService.ConfirmSaleForOrderAsync(@event.OrderId);
    }
}