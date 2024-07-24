using System.Text;
using System.Text.Json;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using RabbitMQ.Client;

namespace ImageVault.AuthenticationService.RabbitMq;

public class RabbitMqMessageSender : IRabbitMqMessageSender
{
    private IRabitMqConnection _connection;

    public RabbitMqMessageSender(IRabitMqConnection connection)
    {
        _connection = connection;  
    }
    
    public void SendMessage<T>(T message,string queue)
    {
       using var channel = _connection.Connection.CreateModel();

       channel.QueueDeclare( queue , true, false);

       var jsonObject = JsonSerializer.Serialize(message);

       var body = Encoding.UTF8.GetBytes(jsonObject);
       
       channel.BasicPublish("", queue , true , body : body );
    }
}