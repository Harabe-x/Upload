using ImageVault.UploadService.Data.Interfaces.RabbitMq;

namespace ImageVault.UploadService.Extension;

public static class IApplicationBuilderExtension
{
    public static IApplicationBuilder UseRabbitMqListener(this IApplicationBuilder app)
    {

        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var listener = scope.ServiceProvider.GetRequiredService<IRabbitMqListener>();

            var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifetime.ApplicationStarted.Register(listener.StartListening);

            lifetime.ApplicationStopped.Register(listener.StopListening);
        }

        return app;
    }
}