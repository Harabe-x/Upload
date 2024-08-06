using System.Text;
using ImageVault.UploadService.Data.Interfaces;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Data.Interfaces.Services;
using ImageVault.UploadService.Extension;
using ImageVault.UploadService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.UploadService.RabbitMq.Consumers;

public class JwtConsumer : IRabbitMqConsumer
{
    private readonly IRabbitMqConnection _connection;

    private readonly IConfiguration _configuration;

    private readonly IJwtTokenProvider _tokenProvider; 

    private  IModel _channel;
    
    public JwtConsumer(IRabbitMqConnection connection, IConfiguration configuration, IJwtTokenProvider tokenProvider)
    {
        _connection = connection;
        _configuration = configuration;
        _tokenProvider = tokenProvider; 
    }
    
    public void Start()
    {
        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetRabbitMqJwtTokenQueueName(), true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HadnleMessage;

        _channel.BasicConsume(_configuration.GetRabbitMqJwtTokenQueueName(), true , consumer);
    }

    private async Task HadnleMessage(object sender, BasicDeliverEventArgs args)
    {
        var message = args.Body.ToArray();

        var jwt = Encoding.UTF8.GetString(message);
        jwt = RemoveQuoteFromString(jwt);

        Console.WriteLine(jwt);
        _tokenProvider.Token = jwt;
    }

    private static string RemoveQuoteFromString(string s)
    {
        return s.Remove(s.Length - 1, 1).Remove(0,1);
    }

    public void Stop()
    {
        _channel.Close();
    }
    public void Dispose()
    {
        _channel.Dispose();
    }
}