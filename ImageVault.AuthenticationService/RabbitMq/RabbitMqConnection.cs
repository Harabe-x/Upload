using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.AuthenticationService.RabbitMq;


/// <summary>
/// <inheritdoc cref="IRabbitMqConnection"/>
/// </summary>
public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    
    /// <summary>
    ///  Object for reading configuration file. 
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    ///  Object for message logging
    /// </summary>
    private readonly ILogger<RabbitMqConnection> _logger;

    public RabbitMqConnection(IConfiguration configuration, ILogger<RabbitMqConnection> logger)
    {
        _configuration = configuration;
        _logger = logger;
        InitializeConnection();
    }

   /// <summary>
   ///  Disposes used resources 
   /// </summary>
    public void Dispose()
    {
        Connection.Dispose();
    }

    public IConnection Connection { get; private set; }

    /// <summary>
    ///  Initializes the connection to AMQP
    /// </summary>
    private void InitializeConnection()
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
        catch (BrokerUnreachableException)
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            InitializeConnection();
            _logger.LogError("Connection to Rabbitmq service failed");
        }
        _logger.LogInformation("Connected to RabbitMq Server");
    }
}