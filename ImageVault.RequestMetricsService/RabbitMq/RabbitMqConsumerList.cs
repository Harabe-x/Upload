using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;
using ImageVault.RequestMetricsService.RabbitMq.Consumers;

namespace ImageVault.RequestMetricsService.RabbitMq;

public class RabbitMqConsumerList : IRabbitMqConsumerList
{
    private readonly List<IRabbitMqConsumer> _consumers;

    public RabbitMqConsumerList(RequestInfoConsumer requestInfoConsumer)
    {
        _consumers = new List<IRabbitMqConsumer>();
        _consumers.Add(requestInfoConsumer);
    }

    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return _consumers.ToList();
    }
}