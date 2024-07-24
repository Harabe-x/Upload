namespace ImageVault.UserService.Data.Interfaces;

public interface IRabbitMqMessageSender
{
     void SendMessage<T>(T message, string queue);
}