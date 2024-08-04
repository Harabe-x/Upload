namespace ImageVault.UploadService.Data.Interfaces;

public interface IRabbitMqConsumer : IDisposable
{
    void Start();

    void Stop();
}