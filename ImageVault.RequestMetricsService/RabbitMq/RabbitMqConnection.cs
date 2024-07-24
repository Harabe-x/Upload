using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace ImageVault.RequestMetricsService.RabbitMq;

public class RabbitMqConnection : IRabbitMqConnection , IDisposable
{
    private readonly IConfiguration _configuration; 
    
    private readonly object _lock = new ();
    
    private IConnection _connection;

    public IConnection Connection
    {
        get
        {
            lock (_lock)
            {
                return _connection; 
            }
        }
    }

    public RabbitMqConnection(IConfiguration configuration)
    {
        _configuration = configuration; 
        InitializeConnection();
    }

    private async void InitializeConnection()
    {
        const int maxAttempts = 5;
        var currentAttempt = 0; 
        
        while ( currentAttempt < maxAttempts &&  _connection is not {} ||  !_connection.IsOpen ) 
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _configuration.GetRabbitMqHostName(),
                    UserName = _configuration.GetRabbitMqUsername(),
                    Password = _configuration.GetRabbitMqPassword()
                };

                lock (_lock)
                { 
                    _connection = factory.CreateConnection();
                }
                
            }
            catch (BrokerUnreachableException e)
            {
                await Task.Delay(TimeSpan.FromSeconds(30));
                currentAttempt += 1;
                if (currentAttempt == maxAttempts) throw;
            }
        }
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}