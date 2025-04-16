namespace CA.Domain.Entities;

public interface IEntityIntegrationEvent
{
    public List<DomainEvent> DomainEvents { get; set; }
    public void ClearDomainEvents();
}