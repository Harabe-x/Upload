using ImageVault.UserService.Data.Interfaces;

namespace ImageVault.UserService.Extension;

public static class ApplicationBuilderExtension
{
    private static IRabbitMqListener _rabbitMqListener { get; set; }

    public static IApplicationBuilder AddRabbitMqConsumer(this IApplicationBuilder app)
    {
        _rabbitMqListener = app.ApplicationServices.GetService<IRabbitMqListener>();

        var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        lifetime.ApplicationStarted.Register(OnStarted);
        lifetime.ApplicationStopped.Register(OnStopped);

        return app;
    }

    public static void OnStarted()
    {
        _rabbitMqListener.Start();
    }

    public static void OnStopped()
    {
        _rabbitMqListener.Stop();
    }
}