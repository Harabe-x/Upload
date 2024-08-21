namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;


/// <summary>
///  A record representing the data returned to the user after authentication
/// </summary>
/// <param name="Email">User email address</param>
/// <param name="Token">Generated JWT token </param>
public record AuthenticationResult
(
    string Email,
    string Token
);