using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.OrderService.Domain.Repositories;
using SabakaMarket.OrderService.Infrastructure.Data;
using SabakaMarket.OrderService.Infrastructure.Repositories;

namespace SabakaMarket.OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}