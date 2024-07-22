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
            FirstName = account.FirstName,
            LastName = account.LastName,
            Email = account.Email,
        };
    }

    public static UserDataDto MapToUserData(this ApplicationUser applicationUser)
    {
        return new UserDataDto(applicationUser.FirstName, applicationUser.LastName,
            applicationUser.PreferedColorSchema, applicationUser.Email, applicationUser.ProfilePicture);
    }
}