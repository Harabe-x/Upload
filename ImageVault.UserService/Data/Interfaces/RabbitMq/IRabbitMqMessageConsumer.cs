namespace ImageVault.UserService.Data.Interfaces;

public interface IRabbitMqMessageConsumer
{
    public Task Start();

    public Task Stop();
}