using System.Collections.Generic;

namespace ImageVault.UserService.Data.Interfaces;

/// <summary>
/// Represents a collection of RabbitMQ consumers, providing access to the individual consumers
/// and methods to retrieve them.
/// </summary>
public interface IRabbitMqConsumerList
{
    /// <summary>
    /// Gets a collection of all RabbitMQ consumers in the list.
    /// </summary>
    /// <value>
    /// An <see cref="IEnumerable{T}"/> of <see cref="IRabbitMqConsumer"/> representing the consumers in the list.
    /// </value>
    IEnumerable<IRabbitMqConsumer> Consumers { get; }

    /// <summary>
    /// Retrieves all RabbitMQ consumers from the list.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="IRabbitMqConsumer"/> representing the consumers.
    /// </returns>
    IEnumerable<IRabbitMqConsumer> GetConsumers();
}
