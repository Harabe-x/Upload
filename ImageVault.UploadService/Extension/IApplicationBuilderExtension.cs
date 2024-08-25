using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ImageVault.UploadService.Extension;

public static class IApplicationBuilderExtension
{
    private static IRabbitMqListener _listener;
    
    public static IApplicationBuilder UseRabbitMqListener(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        
        _listener = scope.ServiceProvider.GetRequiredService<IRabbitMqListener>();
        
        var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        lifetime.ApplicationStarted.Register(OnStarted);

        lifetime.ApplicationStopping.Register(OnStopping);

        return app;
    }

    private static void OnStarted()
    {
        _listener.StartListening();
    }

    private static void OnStopping()
    {
        _listener.StopListening();
    }
}