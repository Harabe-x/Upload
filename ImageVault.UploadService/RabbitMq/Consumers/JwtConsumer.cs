using System.Text;
using ImageVault.UploadService.Data.Interfaces;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Data.Interfaces.Services;
using ImageVault.UploadService.Extension;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.UploadService.RabbitMq.Consumers;


/// <summary>
///  <inheritdoc cref="IRabbitMqConsumer"/>
/// </summary>
public class JwtConsumer : IRabbitMqConsumer
{
    public string Name => "JwtConsumer"; 
    
    public TimeSpan WorkTime => DateTime.Now - StartedAt; 
        
    public DateTime StartedAt { get; private set; }
    
    public bool IsRunning { get; private set; }
    
    private readonly IConfiguration _configuration;
    
    private readonly IRabbitMqConnection _connection;
    
    private readonly IJwtTokenProvider _tokenProvider;
    
    private IModel _channel;

    public JwtConsumer(IRabbitMqConnection connection, IConfiguration configuration, IJwtTokenProvider tokenProvider)
    {
        _connection = connection;
        _configuration = configuration;
        _tokenProvider = tokenProvider;
    }
    
    public void Start()
    {
        StartedAt = DateTime.Now;
        IsRunning = true;
        
        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetRabbitMqJwtTokenQueueName(), true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HadnleMessage;

        _channel.BasicConsume(_configuration.GetRabbitMqJwtTokenQueueName(), true, consumer);
    }

    private async Task HadnleMessage(object sender, BasicDeliverEventArgs args)
    {
        var message = args.Body.ToArray();

        var jwt = Encoding.UTF8.GetString(message);
        jwt = RemoveQuoteFromString(jwt);

        _tokenProvider.Token = jwt;
    }
    
    
    public void Stop()
    {
        IsRunning = false;  
        _channel.Close();
    }

    public void Dispose()
    {
        _channel.Dispose();
    }

    private static string RemoveQuoteFromString(string s)
    {
        return s.Remove(s.Length - 1, 1).Remove(0, 1);
    }
}