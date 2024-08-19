using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.RabbitMq.Consumers;

namespace ImageVault.ImageService.RabbitMq;

public class RabbitMqConsumerList : IRabbitMqConsumerList
{
    public IEnumerable<IRabbitMqConsumer> Consumers => _consumers; 

    private readonly List<IRabbitMqConsumer> _consumers;


    public RabbitMqConsumerList(ImageConsumer imageConsumer)
    {
        _consumers = new List<IRabbitMqConsumer>();
        _consumers.Add(imageConsumer);
        
    } 
    
    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return _consumers;
    }
}