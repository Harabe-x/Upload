using System.Text;
using System.Text.Json;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.UserService.RabbitMq;


/// <summary>
///  <inheritdoc cref="IRabbitMqConsumer"/>
/// </summary>
public class UserDataConsumer : IRabbitMqConsumer
{
    public string Name => "UserDataConsumer";

    public TimeSpan WorkTime => DateTime.Now - StartedAt; 
    
    public DateTime StartedAt { get; private set; }
    
    public bool IsRunning { get; private set; }
    
    private readonly IRabbitMqConnection _connection;

    private readonly ILogger<UserDataConsumer> _logger;

    private IServiceScopeFactory _scopeFactory; 
    
    private IModel _channel; 

    public UserDataConsumer(IRabbitMqConnection connection,
        ILogger<UserDataConsumer> logger, IServiceScopeFactory scopeFactory)
    {
        _connection = connection;
        _scopeFactory = scopeFactory; 
        _logger = logger;
    }

    public async void Start()
    {
        StartedAt = DateTime.Now; 
        IsRunning = true;
        
        _channel  = _connection.Connection.CreateModel();

        _channel.QueueDeclare("UserData", true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;
        _channel.BasicConsume("UserData", false, consumer);
    }

    private async  Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        var json = Encoding.UTF8.GetString(args.Body.ToArray());

        _logger.LogInformation("Message received");

        using var scope = _scopeFactory.CreateAsyncScope();

        var userRepository = scope.ServiceProvider.GetService<IUserRepository>();        
        try
        {
            
            var userData = JsonSerializer.Deserialize<UserData>(json);

            _logger.LogInformation(userData.ToString());
            
            var result = await userRepository.AddUser(userData, userData.Id);

            _logger.LogInformation(result.ToString());
            
            
            if (result.IsSuccess) _channel.BasicAck(args.DeliveryTag, false);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                $"Unexpected exception occured during. Exception : {e.Message}  Source : {e.Source} ");
            _channel.BasicNack(args.DeliveryTag, true, false);
        }
    }

    public async void Stop()
    {
        IsRunning = false; 
        _channel.Close();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}