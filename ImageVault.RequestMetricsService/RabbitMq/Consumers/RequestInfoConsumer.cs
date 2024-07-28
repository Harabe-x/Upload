using System.Text;
using System.Text.Json;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using ImageVault.RequestMetricsService.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ImageVault.RequestMetricsService.RabbitMq.Consumers;

public class RequestInfoConsumer : IRabbitMqConsumer
{
    private readonly IConfiguration _configuration;
    
    private readonly IRabbitMqConnection _connection;

    private readonly ILogger<RequestInfoConsumer> _logger;

    
    private IModel _channel; 

    public RequestInfoConsumer(IRabbitMqConnection connection, IConfiguration configuration, ILogger<RequestInfoConsumer> logger)
    {
        _logger = logger; 
        _connection = connection;
        _configuration = configuration;
    }
    
    
    public void Start()
    {
        _channel = _connection.Connection.CreateModel();

        _channel.QueueDeclare(_configuration.GetRequestQueueName(), true, false);
        
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += HandleMessage;
        
        _channel.BasicConsume(_configuration.GetRequestQueueName(), false , consumer);

    }

    private async Task HandleMessage(object sender, BasicDeliverEventArgs args)
    {
        var jsonMessage = Encoding.UTF8.GetString(args.Body.ToArray());

        try
        {
            var requestObject =JsonSerializer.Deserialize<RequestDto>(jsonMessage);

           
            _logger.LogInformation(requestObject.ToString());
            
            
            _channel.BasicAck(args.DeliveryTag,true);
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Unexpected exception occured during. Exception : {e.Message}  Source : {e.Source} ");
            _channel.BasicNack(args.DeliveryTag, true,  false);
        }
    }
    
    public void Stop()
    {
        _connection?.Connection.Close();
    }

    public void Dispose()
    {
        _connection.Connection?.Dispose();
    }
}