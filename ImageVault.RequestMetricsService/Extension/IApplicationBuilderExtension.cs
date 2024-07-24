using System.Security.Cryptography.Xml;
using ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;

namespace ImageVault.RequestMetricsService.Extension;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder AddRabbitMqListener(this IApplicationBuilder  app)
    {
        var rabbitmqListener = app.ApplicationServices.GetService<IRabbitMqListener>();
        
        var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        lifetime.ApplicationStarted.Register(rabbitmqListener.StartListening);

        lifetime.ApplicationStopped.Register(rabbitmqListener.StopListening);

        return app; 
    }
}