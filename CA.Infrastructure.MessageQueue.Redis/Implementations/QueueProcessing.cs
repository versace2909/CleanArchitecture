using System.Text.Json;
using CA.Domain.Entities;
using CA.Infrastructure.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace CA.Infrastructure.MessageQueue.Redis.Implementations;

public class QueueProcessing(
    ILogger<QueueProcessing> logger,
    IConnectionMultiplexer connection,
    IEventHandlerFactory eventHandlerFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = connection.GetSubscriber();

        await subscriber.SubscribeAsync("queue.name", (channel, value) =>
        {
            if (!value.HasValue) return;
            
            var outboxes = JsonSerializer.Deserialize<List<Outbox>>(value!);
                
            if (outboxes == null || outboxes.Count == 0) return;
                
            foreach (var outbox in outboxes)
            {
                logger.LogInformation("Process message: {Outbox}", outbox);
                
                var handler = eventHandlerFactory.GetEventHandler(outbox.EventName);

                if (handler == null) throw new NullReferenceException("Event handler not found");

                handler.HandleAsync(outbox.Payload).GetAwaiter().GetResult();
            }
        });
    }
}