using System.Net;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;

namespace ImageVault.ApiKeyService.Extension;

public static class ApplicationBuilderExtension
{
    private static IRabbitMqListener _listener;
    
    public static IApplicationBuilder AddRabbitMqListener(this IApplicationBuilder app)
    {
         _listener = app.ApplicationServices.GetService<IRabbitMqListener>();
        var applicationLifetime= app.ApplicationServices.GetService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStarted.Register(OnStarted);

        applicationLifetime.ApplicationStopping.Register(OnStopping);
        
        return app; 
    }

    public static void OnStarted()
    {
        _listener.Start();
    }

    public static void OnStopping()
    {
        _listener.Stop();   
    }
        
    
}