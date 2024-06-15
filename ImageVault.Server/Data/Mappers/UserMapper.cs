using ImageVault.Server.Data.Dtos;
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

    public static UserProfileDto MapToUserProfileDto(this ApplicationUser applicationUser)
    {
        return new UserProfileDto
        { 
            Email =     applicationUser.Email,
            FirstName=  applicationUser.FirstName,
            LastName =     applicationUser.LastName,
            PreferedColorSchema = applicationUser.PreferedColorSchema,
            ProfilePicture =    applicationUser.ProfilePicture
        };

    }

}