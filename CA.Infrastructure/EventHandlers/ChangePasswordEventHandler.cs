using CA.Domain.Events;

namespace CA.Infrastructure.EventHandlers;

public class ChangePasswordEventHandler : BaseEventHandler<ChangePasswordEvent>
{
    public override Task HandleEventAsync(ChangePasswordEvent payload)
    {
        return Task.CompletedTask;
    }
}