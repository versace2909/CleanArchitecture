using CA.Application.Implementations;
using CA.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}