using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using RabbitMQ.Client;

namespace ImageVault.AuthenticationService.RabbitMq;

public class RabitMqConnection : IRabitMqConnection, IDisposable
{

    private  IConnection _connection;

    public  IConnection Connection => _connection;


    public RabitMqConnection()
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
    
    public void Dispose()
    {
        _connection?.Dispose();
    }
}