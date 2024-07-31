namespace ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;

public interface IRabbitMqMessageSender
{
    public void SendMessage<T>(T message, string queue); 
}