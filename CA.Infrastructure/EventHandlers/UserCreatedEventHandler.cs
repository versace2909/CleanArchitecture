using CA.Domain.Events;

namespace CA.Infrastructure.EventHandlers;

public class UserCreatedEventHandler : BaseEventHandler<UserCreatedEvent>
{
    public override Task HandleEventAsync(UserCreatedEvent payload)
    {
        return Task.CompletedTask;
    }
}