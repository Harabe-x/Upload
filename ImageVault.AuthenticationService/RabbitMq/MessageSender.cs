using System.Text;
using System.Text.Json;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using RabbitMQ.Client;

namespace ImageVault.AuthenticationService.RabbitMq;

public class MessageSender : IMessageSender
{
    private IRabitMqConnection _connection;

    public MessageSender(IRabitMqConnection connection)
    {
        _connection = connection;  
    }
    
    public void SendMessage<T>(T message)
    {
       using var channel = _connection.Connection.CreateModel();

       channel.QueueDeclare( "UserData", true, false);

       var jsonObject = JsonSerializer.Serialize(message);

       var body = Encoding.UTF8.GetBytes(jsonObject);
       
       channel.BasicPublish("", "UserData" , true , body : body );
       
       
    }
}