namespace ImageVault.UploadService.Data.Interfaces.RabbitMq;

public interface IRabbitMqListener
{
    IEnumerable<IRabbitMqConsumer> Consumers { get; }

    void StartListening();

    void StopListening();
}