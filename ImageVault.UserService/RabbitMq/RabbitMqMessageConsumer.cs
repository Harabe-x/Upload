using System.Text;
using System.Text.Json;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.UserService.RabbitMq;

public class RabbitMqMessageConsumer : IRabbitMqMessageConsumer
{
    private readonly IRabbitMqConnection _connection;

    private readonly ILogger<RabbitMqMessageConsumer> _logger;

    private readonly IUserRepository _userRepository;

    public RabbitMqMessageConsumer(IRabbitMqConnection connection, IUserRepository userRepository,
        ILogger<RabbitMqMessageConsumer> logger)
    {
        _connection = connection;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Start()
    {
        var channel = _connection.Connection.CreateModel();

        channel.QueueDeclare("UserData", true, false);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());

            _logger.LogInformation("Message received");

            try
            {
                var userData = JsonSerializer.Deserialize<UserDataDto>(json);

                var result = await _userRepository.AddUser(userData, userData.Id);

                if (result.IsSuccess) channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception e)
            {
                _logger.LogCritical(
                    $"Unexpected exception occured during. Exception : {e.Message}  Source : {e.Source} ");
                channel.BasicNack(ea.DeliveryTag, true, false);
            }
        };
        channel.BasicConsume("UserData", false, consumer);
    }

    public async Task Stop()
    {
        _connection?.Connection.Close();
    }
}