using ImageVault.ImageService.Data.Interfaces;

namespace ImageVault.ImageService.RabbitMq;

public class RabbitMqConsumerList : IRabbitMqConsumerList
{
    public IEnumerable<IRabbitMqConsumer> Consumers => _consumers; 

    private readonly List<IRabbitMqConsumer> _consumers;


    public RabbitMqConsumerList()
    {
        _consumers = new List<IRabbitMqConsumer>(); 
        
        
    } 
    
    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return _consumers;
    }
}