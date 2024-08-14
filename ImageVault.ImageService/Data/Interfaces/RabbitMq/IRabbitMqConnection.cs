using RabbitMQ.Client;

namespace ImageVault.ImageService.Data.Interfaces;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}