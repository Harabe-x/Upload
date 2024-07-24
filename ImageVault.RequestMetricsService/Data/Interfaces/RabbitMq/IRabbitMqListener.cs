namespace ImageVault.RequestMetricsService.Data.Interfaces.RabbitMq;

public interface IRabbitMqListener : IDisposable 
{
     IEnumerable<IRabbitMqConsumer> Consumers { get; }

     void StartListening();

     void StopListening();
}

