using ImageVault.Server.Data.Dtos;

namespace ImageVault.Server.Data.Interfaces;

public interface IUserRepository
{
    public Task<UserProfileDto> GetUserData(string id);

    public Task<bool> UpdateUserData(UpdateUserProfileDto newProfileData , string email);

    public Task<bool> DeleteUser(string id);
}