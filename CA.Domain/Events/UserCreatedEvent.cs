using CA.Domain.Entities;

namespace CA.Domain.Events;

public class UserCreatedEvent : DomainEvent
{
    public override string EventName => "UserCreatedEvent";
    public override Guid EventId => Guid.NewGuid();
}