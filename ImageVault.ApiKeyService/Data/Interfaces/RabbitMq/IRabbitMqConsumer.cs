namespace ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;

public interface IRabbitMqConsumer : IDisposable
{
    void Start();

    void Stop();
}