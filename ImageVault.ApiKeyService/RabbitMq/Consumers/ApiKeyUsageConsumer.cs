using System.Text.Json;
using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using ImageVault.ApiKeyService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.ApiKeyService.RabbitMq.Consumers;

public class ApiKeyUsageConsumer : IRabbitMqConsumer
{
    private readonly IConfiguration _configuration;

    private readonly IRabbitMqConnection _rabbitMqConnection;

    private readonly IServiceScopeFactory _scopeFactory;

    private IModel _channel;

    private bool _disposed;

    private readonly ILogger<ApiKeyUsageConsumer> _logger;

    public ApiKeyUsageConsumer(IServiceScopeFactory scopeFactory, IRabbitMqConnection rabbitMqConnection,
        IConfiguration configuration, ILogger<ApiKeyUsageConsumer> logger)
    {
        _rabbitMqConnection = rabbitMqConnection;
        _scopeFactory = scopeFactory;
        _logger = logger;
        _configuration = configuration;
    }

    public void Start()
    {
        _channel = _rabbitMqConnection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetApiKeyUsageQueue(), true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;

        _channel.BasicConsume(_configuration.GetApiKeyUsageQueue(), false, consumer);
    }

    public void Stop()
    {
        _channel.Close();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _channel.Dispose();
        _disposed = true;
    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        var content = args.Body.ToArray();

        try
        {
            var apiKeyUsage = await JsonSerializer.DeserializeAsync<ApiKeyUsageDto>(new MemoryStream(content));

            using var scope = _scopeFactory.CreateAsyncScope();

            var adminService = scope.ServiceProvider.GetService<IAdminApiKeyRepository>();

            if (adminService == null)
            {
                _logger.LogCritical($"ServiceProvider couldn't resolve dependency in {typeof(ApiKeyUsageConsumer)}");
                _channel.BasicNack(args.DeliveryTag, false, true);
                return;
            }

            var result = await adminService.AddUsageToTheApiKey(apiKeyUsage);

            if (!result.IsSuccess)
            {
                _logger.LogCritical(result.Error.message);
                _channel.BasicNack(args.DeliveryTag, false, true);
                return;
            }

            _channel.BasicAck(args.DeliveryTag, false);
        }
        catch (Exception e)
        {
        }
    }
}