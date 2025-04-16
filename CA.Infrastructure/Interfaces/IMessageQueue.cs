namespace CA.Infrastructure.Interfaces;

public interface IMessageQueue
{
    Task<bool> SendMessage<T>(T message, string queueName);
}