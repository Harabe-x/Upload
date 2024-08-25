using System.Text;
using System.Text.Json;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using RabbitMQ.Client;

namespace ImageVault.UploadService.RabbitMq;


/// <summary>
/// <inheritdoc cref="IRabbitMqMessageSender"/>
/// </summary>
public class RabbitMqMessageSender : IRabbitMqMessageSender
{
    private readonly IRabbitMqConnection _connection;

    public RabbitMqMessageSender(IRabbitMqConnection connection)
    {
        _connection = connection;
    }

    public void SendMessage<T>(T message, string queue)
    {
        var channel = _connection.Connection.CreateModel();

        var json = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish("", queue, true, body: body);
    }
}