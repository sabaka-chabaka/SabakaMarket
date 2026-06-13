using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.CatalogService.Domain.Repositories;
using SabakaMarket.CatalogService.Infrastructure.Data;
using SabakaMarket.CatalogService.Infrastructure.Repositories;
using SabakaMarket.CatalogService.Infrastructure.Settings;

namespace SabakaMarket.CatalogService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));

        MongoClassMapper.RegisterMaps();

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}