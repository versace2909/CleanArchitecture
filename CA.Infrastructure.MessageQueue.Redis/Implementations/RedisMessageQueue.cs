using System.Text.Json;
using CA.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CA.Infrastructure.MessageQueue.Redis.Implementations;

public class RedisMessageQueue(ILogger<RedisMessageQueue> logger, IConnectionMultiplexer connection) : IMessageQueue
{
    public async Task<bool> SendMessage<T>(T message, string queueName)
    {
        try
        {
            var subscriber = connection.GetSubscriber();
            await subscriber.PublishAsync(queueName, JsonSerializer.Serialize(message));
            return true;
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
            return false;
        }
    }
}