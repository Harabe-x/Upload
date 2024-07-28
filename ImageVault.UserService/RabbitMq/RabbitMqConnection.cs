using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.UserService.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection , IDisposable
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
                Password = _configuration.GetRabbitMqPassword(),
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
        }
        catch (BrokerUnreachableException e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));    
            InitializeConnection();
            _logger.LogError("Connection to Rabbitmq service failed");
        }
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}