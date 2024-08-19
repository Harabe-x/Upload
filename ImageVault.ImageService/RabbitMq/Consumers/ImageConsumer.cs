using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.Extension;
using ImageVault.ImageService.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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

        _logger.LogInformation("Created Channel");
        
        consumer.Received += HandleMessage;

        _channel.BasicConsume(_configuration.GetImageQueueName(), false, consumer );
    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        using var scope = _scopeFactory.CreateAsyncScope();

        var imageManager = scope.ServiceProvider.GetService<IImageManagerRepository>();

        if (imageManager == null)
        {
            _logger.LogError($"Cannot resolve {typeof(IImageManagerRepository)} Service");
            _channel.BasicNack(args.DeliveryTag, false , true );
        }

        try
        {
            var imageData = await JsonSerializer.DeserializeAsync<ImageDataDto>(new MemoryStream(args.Body.ToArray()));

            var processedImageData = ProcessImageData(imageData);
            
            var result = await imageManager.AddImage(processedImageData);
            

            if (!result.IsSuccess)
            {
                _logger.LogError(result.Error.Message);
            }
        }
        catch (Exception e)
        {
            _logger.LogError($" Exception occured Type : {e.GetType()}  | Message : {e.Message}");
            _channel.BasicNack(args.DeliveryTag, false , true );
        }
        
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

    private ImageDataDto ProcessImageData(ImageDataDto imageDataDto)
    {
        return new ImageDataDto(
            imageDataDto.Key ?? throw new ArgumentException(),
            imageDataDto.ApiKey ?? throw new ArgumentException(),
            imageDataDto.Collection ?? "default", 
            imageDataDto.Title ?? "", 
            imageDataDto.Description ?? "",
            imageDataDto.UserId  ?? throw new ArgumentException(),
            imageDataDto.ImageSize  , 
            imageDataDto.FileFormat ?? ""
        );
    }

}