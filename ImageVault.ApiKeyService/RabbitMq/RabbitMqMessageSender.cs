using System.Text;
using System.Text.Json;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using RabbitMQ.Client;

namespace ImageVault.ApiKeyService.RabbitMq;
/// <summary>
///  <inheritdoc cref="IRabbitMqMessageSender"/>
/// </summary>
public class RabbitMqMessageSender : IRabbitMqMessageSender
{
    private readonly IRabbitMqConnection _connection;

    public RabbitMqMessageSender(IRabbitMqConnection connection)
    {
        _connection = connection;
    }

    public void SendMessage<T>(T message, string queue,string exchange = "")
    {
        var channel = _connection.Connection.CreateModel();

        channel.QueueDeclare(queue, true, false);

        if (exchange != string.Empty)
        {
            channel.ExchangeDeclare(exchange, ExchangeType.Fanout , true);
            channel.QueueBind(queue, exchange,"");
        }
        
        var jsonObject = JsonSerializer.Serialize(message);

        var body = Encoding.UTF8.GetBytes(jsonObject);
        Console.WriteLine($"{queue} {exchange}");
        channel.BasicPublish(exchange, queue, true, body: body);
    }

    
}