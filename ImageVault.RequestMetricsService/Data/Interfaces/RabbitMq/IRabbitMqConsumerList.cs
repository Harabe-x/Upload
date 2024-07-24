namespace ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;

public interface IRabbitMqConsumerList
{
    IEnumerable<IRabbitMqConsumer> GetConsumers();
}