using ImageVault.UserService.Data.Interfaces;

namespace ImageVault.UserService.RabbitMq;

public class RabbitMqListener : IRabbitMqListener
{
    private readonly IRabbitMqConsumerList _consumerList;

    public RabbitMqListener(IRabbitMqConsumerList consumerList)
    {
        _consumerList = consumerList;
        Consumers = _consumerList.GetConsumers();
    }

    public IEnumerable<IRabbitMqConsumer> Consumers { get; }

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