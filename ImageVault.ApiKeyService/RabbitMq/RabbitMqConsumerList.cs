using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using ImageVault.ApiKeyService.RabbitMq.Consumers;

namespace ImageVault.ApiKeyService.RabbitMq;

/// <summary>
/// <inheritdoc cref="IRabbitMqConsumerList"/>
/// </summary>
public class RabbitMqConsumerList : IRabbitMqConsumerList
{
    private readonly List<IRabbitMqConsumer> _consumers;

    public IEnumerable<IRabbitMqConsumer> Consumers => _consumers;

    public RabbitMqConsumerList(ApiKeyUsageConsumer apiKeyUsageConsumer)
    {
        _consumers = new List<IRabbitMqConsumer>();
        _consumers.Add(apiKeyUsageConsumer);
    }
    
    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return _consumers;
    }
}