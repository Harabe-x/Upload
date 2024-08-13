using System.Text;
using System.Text.Json;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.UserService.RabbitMq;

public class UserDataConsumer : IRabbitMqConsumer
{
    private readonly IRabbitMqConnection _connection;

    private readonly ILogger<UserDataConsumer> _logger;

    private readonly IUserRepository _userRepository;

    private IModel _channel;

    public UserDataConsumer(IRabbitMqConnection connection, IUserRepository userRepository,
        ILogger<UserDataConsumer> logger)
    {
        _connection = connection;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async void Start()
    {
        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare("UserData", true, false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;
        _channel.BasicConsume("UserData", false, consumer);
    }

    public async void Stop()
    {
        _connection?.Connection.Close();
    }

    public void Dispose()
    {
    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        var json = Encoding.UTF8.GetString(args.Body.ToArray());

        _logger.LogInformation("Message received");

        try
        {
            var userData = JsonSerializer.Deserialize<UserDataDto>(json);

            var result = await _userRepository.AddUser(userData, userData.Id);

            if (result.IsSuccess) _channel.BasicAck(args.DeliveryTag, false);
        }
        catch (Exception e)
        {
            _logger.LogCritical(
                $"Unexpected exception occured during. Exception : {e.Message}  Source : {e.Source} ");
            _channel.BasicNack(args.DeliveryTag, true, false);
        }
    }
}