using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;
using ImageVault.RequestMetricsService.RabbitMq.Consumers;

namespace ImageVault.RequestMetricsService.RabbitMq;

public class RabbitMqConsumerList : IRabbitMqConsumerList
{
    private readonly List<IRabbitMqConsumer> _consumers;

    public RabbitMqConsumerList(ApiKeyUsageConsumer apiKeyUsageConsumer, ApiKeyLogConsumer apiKeyLogConsumer)
    {
        _consumers = [ apiKeyLogConsumer, apiKeyUsageConsumer];
    }

    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return _consumers.ToList();
    }
}