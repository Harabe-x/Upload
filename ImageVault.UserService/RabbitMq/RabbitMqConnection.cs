using ImageVault.UserService.Data.Interfaces;
using RabbitMQ.Client;

namespace ImageVault.UserService.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection
{
    private IConnection _connection;

    public IConnection Connection => _connection;

    public RabbitMqConnection()
    {
        InitializeConnection();
    }

    private void InitializeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq"
        };

        _connection = factory.CreateConnection();
    }


}