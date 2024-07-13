using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

public record UserDatabaseOperationResultDto
(
    ApplicationUser User,
    bool IsSuccess,
    Error Error
);