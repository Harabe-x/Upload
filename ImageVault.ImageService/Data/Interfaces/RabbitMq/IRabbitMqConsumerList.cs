namespace ImageVault.ImageService.Data.Interfaces;

public interface IRabbitMqConsumerList
{
    IEnumerable<IRabbitMqConsumer> Consumers { get;}
    
    IEnumerable<IRabbitMqConsumer> GetConsumers();
}