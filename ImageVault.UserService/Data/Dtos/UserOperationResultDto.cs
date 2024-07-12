using ImageVault.UserService.Data.Models;

namespace ImageVault.UserService.Data.Dtos;

public record UserOperationResultDto(UserDataDto UserData  , bool IsSuccess,Error Error) ; 