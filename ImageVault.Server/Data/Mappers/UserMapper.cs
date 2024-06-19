using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Dtos.UserDtos;
using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Mappers;

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
            applicationUser.PreferedColorSchema, applicationUser.ProfilePicture);
    }

}