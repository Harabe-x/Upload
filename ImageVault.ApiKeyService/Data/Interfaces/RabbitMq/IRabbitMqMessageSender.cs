namespace ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;


/// <summary>
///   Represents a service responsible for sending messages to a RabbitMQ queue.
/// </summary>
public interface IRabbitMqMessageSender
{
    /// <summary>
    /// Sends a Message of a specified type to the designated RabbitMQ queue.
    /// </summary>
    /// <typeparam name="T">The type of the Message to be sent.</typeparam>
    /// <param name="message">The Message object to be sent to the queue.</param>
    /// <param name="queue">The name of the RabbitMQ queue to which the Message will be sent.</param>
    void SendMessage<T>(T message, string queue);
}
