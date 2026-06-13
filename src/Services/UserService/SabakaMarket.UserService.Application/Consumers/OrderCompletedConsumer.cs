using MassTransit;
using SabakaMarket.Contracts.Orders;
using SabakaMarket.UserService.Domain.Repositories;

namespace SabakaMarket.UserService.Application.Consumers;

public class OrderCompletedConsumer(IUserRepository userRepository) : IConsumer<OrderCompletedEvent>
{
    public async Task Consume(ConsumeContext<OrderCompletedEvent> context)
    {
        var message = context.Message;

        var seller = await userRepository.GetByIdAsync(message.SellerId);
        if (seller is null) return;

        seller.Deposit(message.Amount);

        await userRepository.UpdateAsync(seller);
    }
}