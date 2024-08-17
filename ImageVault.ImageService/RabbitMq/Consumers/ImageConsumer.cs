using System.Runtime.CompilerServices;
using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.Extension;
using ImageVault.ImageService.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.ImageService.RabbitMq.Consumers;

public class ImageConsumer : IRabbitMqConsumer
{

    public string Name => "ImageConsumer";

    public TimeSpan WorkTime => DateTime.Now - StartedAt;
    
    public DateTime StartedAt { get; private set; }
    
    public bool IsRunning { get; private set;  }

    private IModel _channel; 
    
    private readonly IRabbitMqConnection _connection;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IConfiguration _configuration;

    private readonly ILogger<ImageConsumer> _logger; 

    public ImageConsumer(IRabbitMqConnection connection, IServiceScopeFactory scopeFactory,IConfiguration configuration, ILogger<ImageConsumer> logger)
    {
        _connection = connection;
        _scopeFactory = scopeFactory;
        _configuration = configuration;
        _logger = logger;
    }
    
    
    public void Start()
    {
        IsRunning = true;
        StartedAt = DateTime.Now;

        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetImageQueueName() , true ,false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;

        _channel.BasicConsume(_configuration.GetImageQueueName(), false, consumer );
    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        using var scope = _scopeFactory.CreateAsyncScope();

        var imageManager = scope.ServiceProvider.GetService<ImageManagerRepository>();

        if (imageManager == null)
        {
            _logger.LogError($"Cannot resolve {typeof(IImageManagerRepository)} Service");
            _channel.BasicNack(args.DeliveryTag, false , true );
        }
        
        
        // TODO : Add image to the database 
            
        _channel.BasicAck(args.DeliveryTag, false );
    }

    public void Stop()
    {
        IsRunning = false;
        _connection.Connection.Close();
    }
    public void Dispose()
    {
        if (!IsRunning) return;
        
        IsRunning = false;
        _connection.Connection?.Dispose();

    }

}