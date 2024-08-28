using ImageVault.UploadService.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Data.Interfaces.Services;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.RabbitMq;
using ImageVault.UploadService.RabbitMq.Consumers;
using ImageVault.UploadService.Repository;
using ImageVault.UploadService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ImageVault.Tests.ImageService;

public class ImageServiceTestConfiguration
{
    private static readonly Lazy<ImageServiceTestConfiguration> _instance = new (() => new ImageServiceTestConfiguration());

    private readonly IServiceProvider _serviceProvider;

    private ImageServiceTestConfiguration()
    {
        var serviceCollection = new ServiceCollection(); 
        
        serviceCollection.AddSingleton<IAmazonS3Connection, AmazonS3Connection>();
        serviceCollection.AddSingleton<IJwtTokenProvider, JwtTokenProvider>();
        serviceCollection.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
        serviceCollection.AddSingleton<IRabbitMqListener, RabbitMqListener>();
        serviceCollection.AddSingleton<IRabbitMqConsumerList, RabbitMqConsumerList>();
        serviceCollection.AddScoped<IImageUploadRepository, ImageUploadRepository>();
        serviceCollection.AddScoped<IRabbitMqMessageSender, RabbitMqMessageSender>();
        serviceCollection.AddScoped<IImageProcessingService, ImageProcessingService>();
        serviceCollection.AddScoped<IApiKeyRepository, ApiKeyRepository>();
        serviceCollection.AddSingleton<ApiKeyConsumer>();
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public static ImageServiceTestConfiguration Instance => _instance.Value;

    public IServiceProvider GetServiceProvider()
    {
        return _serviceProvider;
    }
}