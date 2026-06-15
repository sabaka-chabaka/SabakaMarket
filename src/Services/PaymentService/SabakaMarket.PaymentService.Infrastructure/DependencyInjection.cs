using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.PaymentService.Domain.Repositories;
using SabakaMarket.PaymentService.Infrastructure.Data;
using SabakaMarket.PaymentService.Infrastructure.Repositories;

namespace SabakaMarket.PaymentService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PaymentDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPaymentRepository, PaymentRepository>();

        return services;
    }
}