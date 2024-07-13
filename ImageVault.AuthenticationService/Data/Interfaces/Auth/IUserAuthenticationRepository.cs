using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;

public interface IUserAuthenticationRepository
{
    public Task<UserDatabaseOperationResultDto> CreateAccount(RegisterAccountDto accountDto);

    public Task<UserDatabaseOperationResultDto> LoginUser(LoginDto loginDto);
}