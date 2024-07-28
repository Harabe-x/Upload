using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;

namespace ImageVault.RequestMetricsService.RabbitMq;

public class RabbitMqListener : IRabbitMqListener
{
    public IEnumerable<IRabbitMqConsumer> Consumers { get; set; }

    private readonly IRabbitMqConsumerList _rabbitMqConsumerList;
    
    public RabbitMqListener(IRabbitMqConsumerList rabbitMqConsumerList)
    {
        _rabbitMqConsumerList = rabbitMqConsumerList;
    }   
    
    public void StartListening()
    {
        Consumers = _rabbitMqConsumerList.GetConsumers();
        
        foreach (var consumer in Consumers)
        {
            consumer.Start();
        }
    }

    public void StopListening()
    {
        foreach (var consumer in Consumers)
        {
            consumer.Stop();
        }
    }

    public void Dispose()
    {
        if (Consumers is not { } || Consumers.Count() == 0) return; 
        
        foreach (var consumer in Consumers)
        {
            consumer?.Dispose();
        }
    }
}