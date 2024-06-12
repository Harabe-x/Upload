using ImageVault.Server.Data.Dtos;

namespace ImageVault.Server.Data.Interfaces;

public interface IUserRepository
{
    public  Task<bool> CreateAccount(UserAccountDto accountDto);
    
    
}