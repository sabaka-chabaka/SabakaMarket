using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.DigitalAssetService.Application.Services;

namespace SabakaMarket.DigitalAssetService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDigitalAssetService, Services.DigitalAssetService>();

        return services;
    }
}