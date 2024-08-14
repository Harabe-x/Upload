namespace ImageVault.ImageService.Data.Interfaces;

public interface IRabbitMqListener : IDisposable
{
    void Start();

    void Stop();
}