namespace ImageVault.UserService.Data.Interfaces;

public interface IRabbitMqConsumerList
{
    IEnumerable<IRabbitMqConsumer> GetConsumers(); 
}