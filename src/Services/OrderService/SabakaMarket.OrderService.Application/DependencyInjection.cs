using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.OrderService.Application.Services;

namespace SabakaMarket.OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, Services.OrderService>();

        return services;
    }
}