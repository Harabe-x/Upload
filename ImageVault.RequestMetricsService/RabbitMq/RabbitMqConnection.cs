using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.RequestMetricsService.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<RabbitMqConnection> _logger;

    public RabbitMqConnection(IConfiguration configuration, ILogger<RabbitMqConnection> logger)
    {
        _configuration = configuration;
        _logger = logger;
        InitializeConnection();
    }

    public void Dispose()
    {
        Connection.Dispose();
    }

    public IConnection Connection { get; private set; }

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

            Connection = factory.CreateConnection();
        }
        catch (BrokerUnreachableException e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            InitializeConnection();
            _logger.LogError("Connection to Rabbitmq service failed");
        }
    }
}