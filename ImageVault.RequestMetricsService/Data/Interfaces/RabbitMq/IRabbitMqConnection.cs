using RabbitMQ.Client; 

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}