using RabbitMQ.Client;

namespace ImageVault.UserService.Data.Interfaces;

public interface IRabbitMqConnection
{
     IConnection Connection { get; } 
}