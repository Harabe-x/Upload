using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Interfaces;

public interface IUserAuthenticationRepository
{
    public  Task<UserDatabaseOperationResultDto> CreateAccount(RegisterAccountDto accountDto);

    public Task<UserDatabaseOperationResultDto> LoginUser(LoginDto loginDto);
}

