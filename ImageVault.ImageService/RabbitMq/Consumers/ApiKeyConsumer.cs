using System.Text.Json;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.ApiKey;
using ImageVault.ImageService.Data.Enums;
using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.Data.Models;
using ImageVault.ImageService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.ImageService.RabbitMq.Consumers;

public class ApiKeyConsumer : IRabbitMqConsumer
{
    public string Name => "ApiKeyConsumer"; 
    
    public DateTime StartedAt { get; private set; }

    public TimeSpan WorkTime => DateTime.Now - StartedAt ;
    
    public bool IsRunning { get; private set; }

    private readonly ILogger<ApiKeyConsumer> _logger;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IConfiguration _configuration;

    private readonly IRabbitMqConnection _connection; 
    
    private IModel _channel;

    private const int RetryInterval = 5; 
    
    public ApiKeyConsumer(ILogger<ApiKeyConsumer> logger, IServiceScopeFactory scopeFactory, IConfiguration configuration,IRabbitMqConnection connection)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _configuration = configuration;
        _connection = connection; 
    }
    
    public void Start()
    {
        StartedAt = DateTime.Now;
        IsRunning = true;
        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetApiKeyQueueName(), true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;
        
        _channel.BasicConsume(_configuration.GetApiKeyQueueName(), false, consumer);
    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            using var scope = _scopeFactory.CreateAsyncScope();

            var apiKeyRepository = scope.ServiceProvider.GetService<IApiKeyRepository>();

            if (apiKeyRepository == null)
            {
                _logger.LogError($"Service provider cannot resolve {typeof(IApiKeyRepository)} in {typeof(ApiKeyConsumer)} ");
                await Task.Delay(TimeSpan.FromSeconds(RetryInterval));
                _channel.BasicNack(args.DeliveryTag, false, true);
                return; 
            }
            
            var apiKey = await JsonSerializer.DeserializeAsync<ApiKeyDto>(new MemoryStream(args.Body.ToArray()));

            var result = apiKey?.OperationType switch
            {
                ApiKeyOperationType.Create => await apiKeyRepository.CreateKey(apiKey),
                ApiKeyOperationType.Delete => await apiKeyRepository.DeleteKey(apiKey),
                ApiKeyOperationType.Edit => await apiKeyRepository.EditKey(apiKey),
                _ => throw new ArgumentOutOfRangeException(nameof(apiKey))
            };

            if (!result.IsSuccess)
            {
                _logger.LogError($"Service provider cannot resolve {typeof(IApiKeyRepository)} in {typeof(ApiKeyConsumer)} ");
                await Task.Delay(TimeSpan.FromSeconds(RetryInterval));
                _channel.BasicNack(args.DeliveryTag, false, true);
                return; 
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            _channel.BasicNack(args.DeliveryTag, false, true);
            return;
        }
        
        _channel.BasicAck(args.DeliveryTag, false);
    }

    public void Stop()
    {
        _channel.Close();
        IsRunning = false;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}