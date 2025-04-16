using CA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;
using CA.Infrastructure.Interfaces;

namespace CA.Infrastructure.Interceptors;

public class SaveChangeInterceptor : SaveChangesInterceptor
{
    private readonly IMessageQueue _messageQueue;

    public SaveChangeInterceptor(IMessageQueue messageQueue)
    {
        _messageQueue = messageQueue;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        SavedChangeSendQueue(eventData, result).GetAwaiter().GetResult();
        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await SavedChangeSendQueue(eventData, result);
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task SavedChangeSendQueue(SaveChangesCompletedEventData eventData, int result)
    {
        var context = eventData.Context;
        if (context == null)
            return;
        
        var entityEntries = context.ChangeTracker.Entries<Outbox>().Select(x=>x.Entity).ToList();
        if (entityEntries.Count == 0)
            return;
        
        await _messageQueue.SendMessage(entityEntries, "queue.name");
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        await Task.CompletedTask;
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<BaseEntity<int>>()
            .Where(e => e.Entity is IEntityIntegrationEvent && ((IEntityIntegrationEvent) e.Entity).DomainEvents.Any())
            .Select(e => e.Entity as IEntityIntegrationEvent).ToArray();

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        var outboxes = new List<Outbox>();
        foreach (var domainEvent in domainEvents)
        {
            outboxes.Add(new Outbox()
            {
                EventName = domainEvent.EventName,
                Payload = JsonSerializer.Serialize(domainEvent),
                CreatedAt = DateTime.UtcNow,
                IsProcessed = false,
                ErrorMessage = string.Empty,
            });
        }

        context.Set<Outbox>().AddRange(outboxes);

    }
}