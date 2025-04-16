using System.ComponentModel.DataAnnotations.Schema;
using CA.Domain.Events;

namespace CA.Domain.Entities;

public class User : BaseEntity<int>, IEntityIntegrationEvent
{
     public string UserName { get; set; }
     public string UserEmail { get; set; }
     public string Password { get; set; }
     public string FirstName { get; set; }
     public string LastName { get; set; }
     public string PhoneNumber { get; set; }
     public string Address { get; set; }

     [NotMapped] public List<DomainEvent> DomainEvents { get; set; } = [];
     
     public void ClearDomainEvents()
     {
          DomainEvents.Clear();
     }
}