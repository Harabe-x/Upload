using RabbitMQ.Client;

namespace ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;


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