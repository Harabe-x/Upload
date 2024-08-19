using ImageVault.UserService.Data.Interfaces;

namespace ImageVault.UserService.RabbitMq;

public class RabbitMqConsumerList : IRabbitMqConsumerList
{

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