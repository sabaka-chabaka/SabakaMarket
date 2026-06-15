using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.DigitalAssetService.Domain.Repositories;
using SabakaMarket.DigitalAssetService.Infrastructure.Data;
using SabakaMarket.DigitalAssetService.Infrastructure.Repositories;

namespace SabakaMarket.DigitalAssetService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DigitalAssetDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDigitalAssetRepository, DigitalAssetRepository>();

        return services;
    }
}