using CA.Domain.Entities;

namespace CA.Domain.Events;

public class ChangePasswordEvent : DomainEvent
{
    public override string EventName => "ChangePasswordEvent";
    public override Guid EventId => Guid.NewGuid();
}