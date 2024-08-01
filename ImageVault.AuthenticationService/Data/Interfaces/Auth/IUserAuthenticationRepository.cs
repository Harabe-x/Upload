using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;

public interface IUserAuthenticationRepository
{
    public Task<OperationResultDto<AuthenticationResultDto>> CreateAccount(RegisterAccountDto accountDto);

    public Task<OperationResultDto<AuthenticationResultDto>> LoginUser(LoginDto loginDto);
}