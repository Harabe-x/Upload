using ImageVault.UserService.Data.Dtos;

namespace ImageVault.UserService.Data.Interfaces;

public interface IUserRepository
{
    Task<OperationResultDto<UserDataDto>> AddUser(UserDataDto userData, string id);

    Task<OperationResultDto<UserDataDto>> GetUser(string email);

    Task<OperationResultDto<UserDataDto>> UpdateUser(UserDataDto newUserData, string id);

    Task<bool> DeleteUser(string email);
}