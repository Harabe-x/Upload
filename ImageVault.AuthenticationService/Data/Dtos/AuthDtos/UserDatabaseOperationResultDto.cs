using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Dtos;

public record UserDatabaseOperationResultDto
(
    ApplicationUser User,
    bool IsSuccess
);
