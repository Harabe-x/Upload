using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;


/// <summary>
///  A repository containing authentication methods
/// </summary>
public interface IUserAuthenticationRepository
{
    
    /// <summary>
    ///  Registers user in the application
    /// </summary>
    /// <param name="account">Account data</param>
    /// <returns>Authentication Result</returns>
    public Task<OperationResult<AuthenticationResult>> CreateAccount(RegisterAccount account);
    
    /// <summary>
    ///  Logs the user into the application 
    /// </summary>
    /// <param name="login">Account data</param>
    /// <returns>Authentication Result</returns>
    public Task<OperationResult<AuthenticationResult>> LoginUser(Login login);
}