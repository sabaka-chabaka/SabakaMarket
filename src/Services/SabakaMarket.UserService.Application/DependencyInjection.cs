using Microsoft.Extensions.DependencyInjection;
using SabakaMarket.UserService.Application.Services;

namespace SabakaMarket.UserService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}