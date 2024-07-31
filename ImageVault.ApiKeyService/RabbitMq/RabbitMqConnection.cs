using System.Runtime.CompilerServices;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using ImageVault.ApiKeyService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.ApiKeyService.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection
{
    private IConnection _connection;

    private readonly IConfiguration _configuration;
    
    public IConnection Connection => _connection;

    private readonly ILogger<RabbitMqConnection> _logger; 

    public  RabbitMqConnection(IConfiguration configuration, ILogger<RabbitMqConnection> logger )
    {
        _logger = logger; 
        _configuration = configuration; 
        
         InitializeConnection();
    }

    private void InitializeConnection()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = _configuration.GetRabbitMqHostName(),
            UserName = _configuration.GetRabbitMqUsername(),
            Password = _configuration.GetRabbitMqPassword(),
            DispatchConsumersAsync = true 
        };

        try
        {
            _connection = connectionFactory.CreateConnection();
        }
        catch (BrokerUnreachableException e)
        {
            _logger.LogError("Connection to Rabbitmq service failed");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            InitializeConnection();
        }
    }
    
}