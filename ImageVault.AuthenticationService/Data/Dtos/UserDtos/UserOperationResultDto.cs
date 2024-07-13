namespace ImageVault.AuthenticationService.Data.Dtos.UserDtos;

public record UserOperationResultDto
(
    UserDataDto UserData,
    bool IsSuccess
);