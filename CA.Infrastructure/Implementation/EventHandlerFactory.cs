using CA.Infrastructure.EventHandlers;
using CA.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure.Implementation;

public class EventHandlerFactory : IEventHandlerFactory
{
    private readonly IServiceProvider _serviceProvider;

    public EventHandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEventHandler? GetEventHandler(string eventName)
    {
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetKeyedService<IEventHandler>(eventName);
        return handler;
    }
}