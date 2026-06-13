using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.CatalogService.Application.Services;

namespace SabakaMarket.CatalogService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICatalogService, Services.CatalogService>();
        
        return services;
    }
}