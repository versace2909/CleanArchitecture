using CA.Domain.Entities;

namespace CA.Infrastructure.EventHandlers;

public interface IEventHandler
{
    public Task HandleAsync(string payload);
}