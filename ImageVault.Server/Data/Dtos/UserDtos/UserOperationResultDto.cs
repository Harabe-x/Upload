namespace ImageVault.Server.Data.Dtos.UserDtos;

public record UserOperationResultDto
(
 UserDataDto UserData,
 bool IsSuccess
 );