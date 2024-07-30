namespace ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;

public interface IRabbitMqMessageSender
{
    void SendMessage<T>(T message, string queue);
}