using CA.Infrastructure.Interfaces;
using CA.Infrastructure.MessageQueue.Redis.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CA.Infrastructure.MessageQueue.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddRedisMessageQueue(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the RedisMessageQueue implementation
        services.AddScoped<IMessageQueue, RedisMessageQueue>();
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration.GetConnectionString("Redis") ?? throw new ArgumentNullException("Redis connection string is not configured.");
            var connection = ConnectionMultiplexer.Connect(connectionString);
            return connection;
        });
        return services;
    }
}