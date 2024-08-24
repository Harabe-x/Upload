using ImageVault.UserService.Data.Interfaces;

namespace ImageVault.UserService.RabbitMq;

/// <summary>
///  <inheritdoc cref="IRabbitMqConsumerList"/>
/// </summary>
public class RabbitMqConsumerList : IRabbitMqConsumerList
{

    public IEnumerable<IRabbitMqConsumer> Consumers => _consumers;
    
    private List<IRabbitMqConsumer> _consumers;

    public RabbitMqConsumerList(UserDataConsumer userDataConsumer)
    {
        _consumers = new List<IRabbitMqConsumer>();

        _consumers.Add(userDataConsumer); 
    }
    
    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return _consumers.ToList();
    }
}