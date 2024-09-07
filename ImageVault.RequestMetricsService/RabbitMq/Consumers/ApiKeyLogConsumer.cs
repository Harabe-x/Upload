using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using ImageVault.RequestMetricsService.Data.Dtos.Log;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.RequestMetricsService.RabbitMq.Consumers;

public class ApiKeyLogConsumer : IRabbitMqConsumer
{
 
    private readonly IConfiguration _configuration;

    private readonly IRabbitMqConnection _connection;

    private readonly IServiceScopeFactory _factory;

    private readonly ILogger<ApiKeyLogConsumer> _logger;

    private IModel _channel;

    public ApiKeyLogConsumer(IRabbitMqConnection connection, IConfiguration configuration,
        ILogger<ApiKeyLogConsumer> logger, IServiceScopeFactory factory)
    {
        _logger = logger;
        _connection = connection;
        _configuration = configuration;
        _factory = factory;
    }


    public void Start()
    {
        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetApiKeyLogQueueName(), true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;

        _channel.BasicConsume(_configuration.GetApiKeyLogQueueName(), false, consumer);
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
        var jsonMessage = Encoding.UTF8.GetString(args.Body.ToArray());

        try
        {
            var apiKeyLog = JsonSerializer.Deserialize<AddApiKeyLog>(jsonMessage);


            _logger.LogInformation(apiKeyLog.ToString());

            using (var scope = _factory.CreateAsyncScope())
            {
                var apiKeyLogsRepository = scope.ServiceProvider.GetService<IApiKeyLogsRepository>();


                await apiKeyLogsRepository.AddLog(apiKeyLog.ApiKey, apiKeyLog.Message);
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