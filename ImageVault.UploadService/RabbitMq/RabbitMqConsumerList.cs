using ImageVault.UploadService.Data.Interfaces;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.RabbitMq.Consumers;

namespace ImageVault.UploadService.RabbitMq;

public class RabbitMqConsumerList : IRabbitMqConsumerList
{
    private readonly List<IRabbitMqConsumer> Consumers;

    public RabbitMqConsumerList(JwtConsumer jwtConsumer)
    {
        Consumers = new List<IRabbitMqConsumer>();

        Consumers.Add(jwtConsumer);
    }

    public IEnumerable<IRabbitMqConsumer> GetConsumers()
    {
        return Consumers.ToList();
    }
}