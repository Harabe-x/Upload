using ImageVault.UploadService.Data.Interfaces;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;

namespace ImageVault.UploadService.RabbitMq;

/// <summary>
/// <inheritdoc cref="IRabbitMqListener"/>
/// </summary>
public class RabbitMqListener : IRabbitMqListener
{
    private readonly IRabbitMqConsumerList _rabbitMqConsumerList;

    public RabbitMqListener(IRabbitMqConsumerList rabbitMqConsumerList)
    {
        _rabbitMqConsumerList = rabbitMqConsumerList;
    }

    public IEnumerable<IRabbitMqConsumer> Consumers { get; set; }

    public void StartListening()
    {
        Consumers = _rabbitMqConsumerList.GetConsumers();

        foreach (var consumer in Consumers) consumer.Start();
    }

    public void StopListening()
    {
        foreach (var consumer in Consumers) consumer.Stop();
    }

    public void Dispose()
    {
        if (Consumers == null || Consumers.Count() == 0) return;

        foreach (var consumer in Consumers) consumer?.Dispose();
    }
}