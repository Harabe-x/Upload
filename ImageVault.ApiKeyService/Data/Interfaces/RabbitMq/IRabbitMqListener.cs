namespace ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;

public interface IRabbitMqListener : IDisposable
{
    void Start();

    void Stop();
}
