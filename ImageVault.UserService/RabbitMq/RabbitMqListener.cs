using ImageVault.UserService.Data.Interfaces;

namespace ImageVault.UserService.RabbitMq;

/// <summary>
///  <inheritdoc cref="IRabbitMqListener"/>
/// </summary>
public class RabbitMqListener : IRabbitMqListener
{
    public IEnumerable<IRabbitMqConsumer> Consumers { get; }

    private readonly IRabbitMqConsumerList _consumerList; 
    
    public RabbitMqListener(IRabbitMqConsumerList consumerList)
    {
        _consumerList = consumerList;
        Consumers = _consumerList.GetConsumers();
    }
    
    public void Start()
    {
        foreach (var consumer in Consumers) consumer.Start();
    }

    public void Stop()
    {
        foreach (var consumer in Consumers) consumer.Stop();
    }

    public void Dispose()
    {
        if (Consumers == null || Consumers.Count() == 0) return;

        foreach (var consumer in Consumers) consumer?.Dispose();
    }
}