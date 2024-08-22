using System.Net;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;

namespace ImageVault.ApiKeyService.Extension;

/// <summary>
/// Provides extension methods for configuring the application pipeline with RabbitMQ listener support.
/// </summary>
public static class ApplicationBuilderExtension
{
    private static IRabbitMqListener _listener;

    /// <summary>
    /// Adds a RabbitMQ listener to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance to configure.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance with RabbitMQ listener configured.</returns>
    public static IApplicationBuilder AddRabbitMqListener(this IApplicationBuilder app)
    {
        _listener = app.ApplicationServices.GetService<IRabbitMqListener>();
        var applicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        applicationLifetime.ApplicationStarted.Register(OnStarted);
        applicationLifetime.ApplicationStopping.Register(OnStopping);
        
        return app;
    }

    /// <summary>
    /// Called when the application has started. Starts the RabbitMQ listener.
    /// </summary>
    public static void OnStarted()
    {
        _listener.Start();
    }

    /// <summary>
    /// Called when the application is stopping. Stops the RabbitMQ listener.
    /// </summary>
    public static void OnStopping()
    {
        _listener.Stop();
    }
}
