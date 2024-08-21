using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Mappers;


/// <summary>
///  Class responsible for mapping user data 
/// </summary>
public static class UserMapper
{
    /// <summary>
    ///  Maps RegisterAccount to ApplicationUSer
    /// </summary>
    /// <param name="account"></param>
    /// <returns>Mapped application user</returns>
    public static ApplicationUser MapUser(this RegisterAccount account)
    {
        return new ApplicationUser
        {
            Email = account.Email
        };
    }

    /// <summary>
    ///   Maps RegisterAccount to UserData
    /// </summary>
    /// <param name="applicationUser"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static UserData MapToUserData(this RegisterAccount applicationUser, string id)
    {
        return new UserData(id, applicationUser.FirstName, applicationUser.LastName,
            applicationUser.DataTheme, applicationUser.Email, applicationUser.ProfilePictureUrl);
    }
}