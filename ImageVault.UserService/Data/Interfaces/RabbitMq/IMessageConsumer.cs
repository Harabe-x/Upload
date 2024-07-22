namespace ImageVault.UserService.Data.Interfaces;

public interface IMessageConsumer
{
    public void Register();

    public void Unregister(); 
}