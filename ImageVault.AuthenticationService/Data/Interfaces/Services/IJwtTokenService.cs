using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;

/// <summary>
/// A Service responsible for creating JWT tokens
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user with an assigned role.
    /// </summary>
    /// <param name="user">The user for whom the token is being created.</param>
    /// <param name="role">The role of the user that will be encoded into the token, such as "Admin" or "User".</param>
    /// <returns>Returns a signed JWT token as a string.</returns>
    public string CreateToken(ApplicationUser user, string role);
}