namespace ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;

public interface IMessageSender
{
    void SendMessage<T>(T message);
}