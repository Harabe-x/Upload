namespace ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;

public interface IRabbitMqConsumerList
{
  IEnumerable<IRabbitMqConsumer> Consumers { get; }

  IEnumerable<IRabbitMqConsumer> GetConsumers();
  
}