using MassTransit;
using SabakaMarket.Contracts.Payments;
using SabakaMarket.OrderService.Application.Services;

namespace SabakaMarket.OrderService.Application.Consumers;

public class PaymentConfirmedConsumer(IOrderService orderService) : IConsumer<PaymentConfirmedEvent>
{
    public async Task Consume(ConsumeContext<PaymentConfirmedEvent> context)
    {
        var @event = context.Message;

        await orderService.PayOrderAsync(@event.OrderId);
    }
}