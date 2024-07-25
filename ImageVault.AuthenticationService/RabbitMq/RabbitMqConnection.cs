using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.AuthenticationService.RabbitMq;

public class RabbitMqConnection : IRabitMqConnection, IDisposable
{
    private readonly IConfiguration _configuration; 
    
    private readonly ILogger<RabbitMqConnection> _logger; 
    
    private IConnection _connection;

    public IConnection Connection => _connection; 
    
    public RabbitMqConnection(IConfiguration configuration,ILogger<RabbitMqConnection> logger )
    {
        _configuration = configuration;
        _logger = logger;
        InitializeConnection();
    }

    private async void InitializeConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetRabbitMqHostName(),
                UserName = _configuration.GetRabbitMqUsername(),
                Password = _configuration.GetRabbitMqPassword()
            };

            _connection = factory.CreateConnection();
        }
        catch (BrokerUnreachableException e)
        {
            await Task.Delay(TimeSpan.FromSeconds(30));
            InitializeConnection();
            _logger.LogError("Connection to Rabbitmq service failed");
        }
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}