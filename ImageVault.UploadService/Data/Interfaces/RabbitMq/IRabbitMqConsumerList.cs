namespace ImageVault.UploadService.Data.Interfaces.RabbitMq;

public interface IRabbitMqConsumerList
{
    IEnumerable<IRabbitMqConsumer> GetConsumers();
}