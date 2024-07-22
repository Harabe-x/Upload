using RabbitMQ.Client;

namespace ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;

public interface IRabitMqConnection
{
    IConnection Connection { get; } 
}