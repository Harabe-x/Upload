using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Enums;
using ImageVault.UserService.Data.Models;

namespace ImageVault.UserService.Data.Mappers;


/// <summary>
///  User Mapper 
/// </summary>
public static class UserMapper
{
    
    /// <summary>
    ///  This method is responsible for mapping <see cref="UserModel"/> to <see cref="UserData"/> 
    /// </summary>
    /// <param name="user">mapped object</param>
    /// <returns></returns>
    public static UserData MapToUserDataDto(this UserModel user)
    {
        return new UserData(user.Id, user.FirstName, user.LastName, user.ColorSchema.ToString(), user.Email,
            user.ProfilePictureUrl);
    }

    /// <summary>
    ///  This method is responsible for mapping <see cref="UserData"/> to <see cref="UserModel"/> 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static UserModel MapToUserModel(this UserData user, string id)
    {
        return new UserModel
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Email = user.Email,
            ColorSchema = Enum.Parse<ApplicationColorSchemas>(user.DataTheme.ToLower())
        };
    }
}