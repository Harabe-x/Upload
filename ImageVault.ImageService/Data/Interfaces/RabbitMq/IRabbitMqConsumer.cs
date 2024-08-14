namespace ImageVault.ImageService.Data.Interfaces;

public interface IRabbitMqConsumer : IDisposable
{
    void Start();

    void Stop();
}