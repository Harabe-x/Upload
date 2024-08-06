namespace ImageVault.UserService.Data.Interfaces;

public interface IRabbitMqListener : IDisposable
{
    IEnumerable<IRabbitMqConsumer> Consumers { get; }

    void Start();

    void Stop();
}