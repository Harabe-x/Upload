using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Dtos.UserDtos;
using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Mappers;

public static class UserMapper
{
    public static ApplicationUser MapUser(this RegisterAccountDto account)
    {
        return new ApplicationUser
        {
            Email = account.Email,
        };
    }

    public static UserDataDto MapToUserData(this RegisterAccountDto applicationUser,string id)
    {
        return new UserDataDto(id,applicationUser.FirstName, applicationUser.LastName,
            applicationUser.DataTheme, applicationUser.Email, applicationUser.profilePictureUrl);
    }
}