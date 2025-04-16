using System.Text.Json;
using CA.Domain.Entities;

namespace CA.Infrastructure.EventHandlers;

public abstract class BaseEventHandler<T> : IEventHandler where T : DomainEvent
{
    public abstract Task HandleEventAsync(T payload);
    public async Task HandleAsync(string payload)
    {
        var eventData = JsonSerializer.Deserialize<T>(payload) ?? throw new InvalidOperationException("Payload is null");
        await HandleEventAsync(eventData);
    }
}