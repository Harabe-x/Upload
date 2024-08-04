using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;

public interface IJwtTokenService
{
    public string CreateToken(ApplicationUser user);
}