namespace ImageVault.UserService.Data.Interfaces;

public interface IRabbitMqConsumer : IDisposable
{
    void Start();

    void Stop();
}