namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IRabbitMqConsumer : IDisposable
{
    void Start();

    void Stop();
}