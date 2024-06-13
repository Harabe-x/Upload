using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Dtos;

public class RegistrationResultDto
{
    public ApplicationUser User { get; set; }

    public bool IsSuccess { get; set; } 


}