using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.ImageService.RabbitMq;

/// <summary>
///  <inheritdoc cref="IRabbitMqConnection"/>
/// </summary>
public class RabbitMqConnection : IRabbitMqConnection
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<RabbitMqConnection> _logger;

    public RabbitMqConnection(IConfiguration configuration, ILogger<RabbitMqConnection> logger)
    {
        _logger = logger;
        _configuration = configuration;

        InitializeConnection();
    }

    public IConnection Connection { get; private set; }

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
            Connection = connectionFactory.CreateConnection();
        }
        catch (BrokerUnreachableException)
        { 
            _logger.LogError("Connection to Rabbitmq service failed");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            InitializeConnection();
        }
    }
}