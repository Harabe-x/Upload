

using ImageVault.UserService.Data.Dtos;

namespace ImageVault.UserService.Data.Interfaces;

public interface IUserRepository
{
    Task<UserOperationResultDto> AddUser(UserDataDto userData, string id);
    
    Task<UserOperationResultDto> GetUser(string email);
    
    Task<UserOperationResultDto> UpdateUser(UserDataDto newUserData , string id);
    
    Task<bool> DeleteUser(string email);

}