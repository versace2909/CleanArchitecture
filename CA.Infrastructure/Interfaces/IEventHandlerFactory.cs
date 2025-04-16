
using CA.Infrastructure.EventHandlers;

namespace CA.Infrastructure.Interfaces;

public interface IEventHandlerFactory
{
    IEventHandler? GetEventHandler(string eventName);
}