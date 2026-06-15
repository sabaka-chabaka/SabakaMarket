using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.PaymentService.Application.Services;

namespace SabakaMarket.PaymentService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, Services.PaymentService>();

        return services;
    }
}