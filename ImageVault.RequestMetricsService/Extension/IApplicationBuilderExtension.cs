using ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;

namespace ImageVault.RequestMetricsService.Extension;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder AddRabbitMqListener(this IApplicationBuilder app)
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