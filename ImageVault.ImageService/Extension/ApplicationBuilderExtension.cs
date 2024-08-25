using ImageVault.ImageService.Data.Interfaces;

namespace ImageVault.ImageService.Extension;

// <summary>
/// Provides extension methods for configuring the application pipeline with RabbitMQ listener support.
/// </summary>
public static class ApplicationBuilderExtension
{
    private static IRabbitMqListener _rabbitMqListener { get;  set; }

    /// <summary>
    /// Adds a RabbitMQ listener to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance to configure.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance with RabbitMQ listener configured.</returns>
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