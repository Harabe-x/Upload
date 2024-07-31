using RabbitMQ.Client;

namespace ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;

public interface IRabbitMqConnection
{
    IConnection Connection { get; } 
}