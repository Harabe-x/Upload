using System.Security.Claims;

namespace ImageVault.Server.Data.Dtos;

public class TokenValidationDto
{
    public ClaimsPrincipal? Claims { get; set; }
    
    public bool IsSuccess { get; set; }
    
    
}