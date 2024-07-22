using System.Text;
using System.Text.Json;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Interfaces;
using RabbitMQ.Client.Events;

namespace ImageVault.UserService.RabbitMq;

public class MessageConsumer : IMessageConsumer
{
    private readonly IRabbitMqConnection _connection;

    private readonly IUserRepository _userRepository; 

    public MessageConsumer(IRabbitMqConnection connection, IUserRepository userRepository)
    {
        _connection = connection;
        _userRepository = userRepository; 
    }
    public void Register ()
    {
     var channel =  _connection.Connection.CreateModel();

     channel.QueueDeclare("UserData",true, false, false, null);

     var consumer = new EventingBasicConsumer(channel);

     consumer.Received += async (model, ea) =>
     {
         var json = Encoding.UTF8.GetString(ea.Body.ToArray());

         try
         {
             var userData = JsonSerializer.Deserialize<UserDataDto>(json);

             var result = await _userRepository.AddUser(userData, "id");

             if (result.IsSuccess)
             {
                 channel.BasicAck(ea.DeliveryTag, false);
             }
         }
         catch (Exception e)
         {
             // Todo 
         }
     };
     channel.BasicConsume("UserData", false , string.Empty, true,false,null,consumer);
    }

    public void Unregister()
    {
        _connection?.Connection.Close();
    }

}
