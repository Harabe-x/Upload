using RabbitMQ.Client;

namespace ImageVault.UploadService.Data.Interfaces.RabbitMq;

public interface IRabbitMqConnection
{
    public IConnection Connection { get; }
}