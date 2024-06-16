using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Models;
using Microsoft.IdentityModel.Tokens;

namespace ImageVault.Server.Services;

public class TokenService : ITokenService
{

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
    }

    public string CreateToken(ApplicationUser user)
    {

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Sub, user.Id),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


    public TokenValidationDto ValidateToken(string token)
    {

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["JWT:Issuer"],
            ValidAudience = _configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]))
        };

        try
        {        
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenClaimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _ );

            if (tokenClaimsPrincipal == null)
            {
                return new TokenValidationDto(null, false);
            }

            return new TokenValidationDto(tokenClaimsPrincipal, true);
        }
        catch(Exception e)
        {
            return new TokenValidationDto(null, false);
        }
        
    }

    private readonly IConfiguration _configuration;

    private readonly SymmetricSecurityKey _key; 
}