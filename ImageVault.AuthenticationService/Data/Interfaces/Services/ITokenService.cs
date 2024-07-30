using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;

public interface ITokenService
{
    public string CreateToken(ApplicationUser user);
}