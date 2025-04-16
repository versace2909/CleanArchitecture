using CA.Domain.Events;
using CA.Infrastructure.EventHandlers;
using CA.Infrastructure.Implementation;
using CA.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here
        // e.g., services.AddDbContext<ApplicationDbContext>(options => ...);
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();
        
        services.AddKeyedScoped<IEventHandler, UserCreatedEventHandler>("UserCreatedEvent");
        services.AddKeyedScoped<IEventHandler, ChangePasswordEventHandler>("ChangePasswordEvent");

        return services;
    }
}