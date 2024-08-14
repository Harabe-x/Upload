using ImageVault.ImageService.Data.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ImageVault.ImageService.RabbitMq;

public class RabbitMqListener : IRabbitMqListener
{

    private readonly IRabbitMqConsumerList _consumerList;

    private IEnumerable<IRabbitMqConsumer> _consumers => _consumerList.GetConsumers(); 
    
    public RabbitMqListener(IRabbitMqConsumerList consumerList)
    {
        _consumerList = consumerList;
    }
    
    public void Start()
    {
        foreach (var consumer in _consumers) consumer.Start();  
    }

    public void Stop()
    {
        foreach (var consumer in _consumers) consumer.Stop();
    }

    public void Dispose()
    {
        foreach(var consumer in _consumers) consumer.Dispose();
    }
}