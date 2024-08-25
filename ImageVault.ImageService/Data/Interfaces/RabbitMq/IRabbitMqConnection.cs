using RabbitMQ.Client;

namespace ImageVault.ImageService.Data.Interfaces;

/// <summary>
///   Represents a service responsible for connecting to RabbitMQ server.
/// </summary>
public interface IRabbitMqConnection
{
    /// <summary>
    ///  RabbitMq server connection
    /// </summary>
    IConnection Connection { get; }
}