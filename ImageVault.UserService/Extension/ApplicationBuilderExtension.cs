using ImageVault.UserService.Data.Interfaces;

namespace ImageVault.UserService.Extension;

public static class ApplicationBuilderExtension
{
    private static IRabbitMqMessageConsumer _rabbitMqConsumer { get; set; }

    public static IApplicationBuilder AddRabbitMqConsumer(this IApplicationBuilder app)
    {
        _rabbitMqConsumer = app.ApplicationServices.GetService<IRabbitMqMessageConsumer>();

        var lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

        lifetime.ApplicationStarted.Register(OnStarted);
        lifetime.ApplicationStopped.Register(OnStopped);

        return app;
    }

    public static void OnStarted()
    {
        _rabbitMqConsumer.Start();
    }

    public static void OnStopped()
    {
        _rabbitMqConsumer.Stop();
    }
}