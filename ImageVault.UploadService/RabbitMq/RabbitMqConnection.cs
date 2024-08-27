using Azure.Identity;
using ImageVault.UploadService.Configuration;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.UploadService.RabbitMq;


/// <summary>
/// <inheritdoc cref="IRabbitMqConnection"/>
/// </summary>
public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private readonly IConfiguration _configuration;

    private readonly ILogger<RabbitMqConnection> _logger;

    public IConnection Connection { get; private set; }
    
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
    
    private async void InitializeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration.GetRabbitMqHostName(),
            UserName = EnvironmentVariables.GetRabbitMqUsername(),
            Password = EnvironmentVariables.GetRabbitMqPassword(),
            DispatchConsumersAsync = true
        };

        try
        {
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