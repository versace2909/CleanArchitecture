using System.Text.Json.Serialization;
using CA.Domain.Events;

namespace CA.Domain.Entities;

[JsonDerivedType(typeof(ChangePasswordEvent))]
[JsonDerivedType(typeof(UserCreatedEvent))]
public abstract class DomainEvent
{
    public abstract string EventName { get; }
    public abstract Guid EventId { get; }
}