using System.Text;
using System.Text.Json;
using ImageVault.UserService.Data.Interfaces;
using RabbitMQ.Client;

namespace ImageVault.UserService.RabbitMq;

public class RabbitMqMessageSender : IRabbitMqMessageSender
{
    private readonly IRabbitMqConnection _connection;

    public RabbitMqMessageSender(IRabbitMqConnection connection)
    {
        _connection = connection;
    }

    public void SendMessage<T>(T message, string queue)
    {
        using var channel = _connection.Connection.CreateModel();

        channel.QueueDeclare(queue, true, false);

        var jsonObject = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonObject);

        channel.BasicPublish("", queue, true, body: body);
    }
}