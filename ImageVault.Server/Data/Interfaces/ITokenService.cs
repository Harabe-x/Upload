using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Interfaces;

public interface ITokenService
{
    public string CreateToken(ApplicationUser user);

    public TokenValidationDto ValidateToken(string token);

}