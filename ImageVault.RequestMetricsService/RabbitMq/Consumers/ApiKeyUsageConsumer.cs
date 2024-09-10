using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.RequestMetricsService.RabbitMq.Consumers;

public class ApiKeyUsageConsumer : IRabbitMqConsumer
{ 
    
    private readonly IConfiguration _configuration;

    private readonly IRabbitMqConnection _connection;

    private readonly IServiceScopeFactory _factory;

    private readonly ILogger<ApiKeyUsageConsumer> _logger;

    private IModel _channel;

    public ApiKeyUsageConsumer(IRabbitMqConnection connection, IConfiguration configuration,
        ILogger<ApiKeyUsageConsumer> logger, IServiceScopeFactory factory)
    {
        _logger = logger;
        _connection = connection;
        _configuration = configuration;
        _factory = factory;
    }


    public void Start()
    {
        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetApiKeyResourceUsageQueue(), true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;

        _channel.BasicConsume(_configuration.GetApiKeyResourceUsageQueue(), false, consumer);
    }

    public void Stop()
    {
        _connection?.Connection.Close();
    }

    public void Dispose()
    {
        _connection.Connection?.Dispose();
    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {

        try
        {
            var usageMetrics = await JsonSerializer.DeserializeAsync<AddUsageMetrics>(new MemoryStream(args.Body.ToArray()));
            
            _logger.LogInformation(usageMetrics.ToString());

            using (var scope = _factory.CreateAsyncScope())
            {
                var usageCollector = scope.ServiceProvider.GetService<IUsageCollectorRepository>();
                
                await usageCollector.AddStorageUsage(usageMetrics.BytesUsed,usageMetrics.UserId);
                
                await usageCollector.IncrementTotalUploadedImages(usageMetrics.UserId);
            }

            _channel.BasicAck(args.DeliveryTag, true);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Unexpected exception occured during. Exception : {e.Message}  Source : {e.Source} ");
            _channel.BasicNack(args.DeliveryTag, true, false);
        }
    }
}