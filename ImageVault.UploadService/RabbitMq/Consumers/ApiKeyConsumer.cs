using System.Security.Cryptography;
using System.Text.Json;
using ImageVault.UploadService.Data.Dtos.ApiKey;
using ImageVault.UploadService.Data.Enums;
using ImageVault.UploadService.Data.Interfaces;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.UploadService.RabbitMq.Consumers;

/// <summary>
/// <inheritdoc cref="IRabbitMqConsumer"/>
/// </summary>
public class ApiKeyConsumer : IRabbitMqConsumer
{
    public string Name => "ApiKeyConsumer";

    public TimeSpan WorkTime => DateTime.Now - StartedAt;
    
    public DateTime StartedAt { get; private set; }
    
    public bool IsRunning { get; private set;  }

    private IModel _channel; 
    
    private readonly IRabbitMqConnection _connection;

    private readonly IConfiguration _configuration;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<ApiKeyConsumer> _logger;

    public ApiKeyConsumer(IRabbitMqConnection connection, IConfiguration configuration,
        IServiceScopeFactory scopeFactory , ILogger<ApiKeyConsumer> logger)
    {

        _connection = connection; 
        _configuration = configuration;
        _scopeFactory = scopeFactory; 
        _logger = logger;
    }

    public void Start()
    { 
        var queue = Guid.NewGuid().ToString();

        IsRunning = true;
        StartedAt = DateTime.Now;
        
        _channel = _connection.Connection.CreateModel(); 
        
         _channel.ExchangeDeclare(_configuration.GetApiKeyExchangeName() , ExchangeType.Fanout, true);

         _channel.QueueDeclare(queue, true , false);

         _channel.QueueBind(queue, _configuration.GetApiKeyExchangeName(), string.Empty);
         
         var consumer = new AsyncEventingBasicConsumer(_channel);

         consumer.Received += HandleMessage;

         _channel.BasicConsume(queue, false, consumer);
    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        const int retryInterval = 3;

        try
        {
            using var scope = _scopeFactory.CreateAsyncScope();

            var apiKeyRepository = scope.ServiceProvider.GetService<IApiKeyRepository>();

            if (apiKeyRepository == null)
            {
                _logger.LogError($"An error occurred while resolving {typeof(IApiKeyRepository)} ");
                await Task.Delay(TimeSpan.FromSeconds(retryInterval));
                _channel.BasicNack(args.DeliveryTag, false, true);
                return;
            }

            var apiKey = await JsonSerializer.DeserializeAsync<ApiKey>(new MemoryStream(args.Body.ToArray()));

            var result = apiKey?.OperationType switch
            {
                ApiKeyOperationType.Create => await apiKeyRepository.CreateKey(apiKey),
                ApiKeyOperationType.Delete => await apiKeyRepository.DeleteKey(apiKey),
                ApiKeyOperationType.Edit => await apiKeyRepository.EditKey(apiKey),
                _ => throw new ArgumentOutOfRangeException(nameof(apiKey))
            };

            if (!result.IsSuccess)
            {
                _logger.LogError($"An error occurred while adding a key to db");
                await Task.Delay(TimeSpan.FromSeconds(retryInterval));
                _channel.BasicNack(args.DeliveryTag, false, true);
            }
            
            _channel.BasicAck(args.DeliveryTag,false);
        }
        catch (Exception e)
        {
            _logger.LogError($"Some error occurred {e} ");
        }
    }

    public void Stop()
    {
        IsRunning = false; 
        
        _channel.Close();
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
