using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Interfaces;

public interface IUserRepository
{
    public  Task<RegistrationResultDto> CreateAccount(UserAccountDto accountDto);
}

