using ImageVault.Server.Data.Dtos.UserDtos;

namespace ImageVault.Server.Data.Interfaces.User;

public interface IUserRepository
{
    Task<UserOperationResultDto> GetUser(string email);

    Task<UserOperationResultDto> UpdateUser(UserDataDto newUserData , string email);

    Task<bool> DeleteUser(string email);

}