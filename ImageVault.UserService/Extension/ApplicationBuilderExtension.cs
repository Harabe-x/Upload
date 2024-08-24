using ImageVault.UserService.Data.Interfaces;

namespace ImageVault.UserService.Extension;


/// <summary>
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
    public static IApplicationBuilder AddRabbitMqConsumer(this IApplicationBuilder app)
    {
        _rabbitMqListener = app.ApplicationServices.GetService<IRabbitMqListener>();

        var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        lifetime.ApplicationStarted.Register(OnStarted);
        lifetime.ApplicationStopped.Register(OnStopped);

        return app;
    }

    /// <summary>
    /// Called when the application has started. Starts the RabbitMQ listener.
    /// </summary>
    public static void OnStarted()
    {
        _rabbitMqListener.Start();
    }

    /// <summary>
    /// Called when the application is stopping. Stops the RabbitMQ listener.
    /// </summary>
    public static void OnStopped()
    {
        _rabbitMqListener.Stop();
    }
}