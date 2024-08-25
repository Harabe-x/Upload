using ImageVault.UploadService.Data.Interfaces;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.RabbitMq.Consumers;

namespace ImageVault.UploadService.RabbitMq;


/// <summary>
///  <inheritdoc cref="IRabbitMqConsumerList"/>
/// </summary>
public class RabbitMqConsumerList : IRabbitMqConsumerList
{
    private readonly List<IRabbitMqConsumer>_consumers;

    public IEnumerable<IRabbitMqConsumer> Consumers => _consumers; 
    
    public RabbitMqConsumerList(JwtConsumer jwtConsumer)
    {
        _consumers = new List<IRabbitMqConsumer>();
        _consumers.Add(jwtConsumer);
    }

    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return _consumers;
    }
}