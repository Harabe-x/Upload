using ImageVault.ImageService.Data.Interfaces;

namespace ImageVault.ImageService.Extension;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder UseRabbitMqListener(this IApplicationBuilder app)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var listener = scope.ServiceProvider.GetRequiredService<IRabbitMqListener>();

            var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifetime.ApplicationStarted.Register(listener.Start);

            lifetime.ApplicationStopped.Register(listener.Stop);
        }

        return app;
    }

}