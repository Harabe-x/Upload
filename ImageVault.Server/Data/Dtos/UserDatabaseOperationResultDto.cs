using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Dtos;

public class UserDatabaseOperationResultDto
{
    public ApplicationUser User { get; set; }

    public bool IsSuccess { get; set; } 


}