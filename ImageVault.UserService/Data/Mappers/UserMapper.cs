using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Enums;
using ImageVault.UserService.Data.Models;

namespace ImageVault.UserService.Data.Mappers;

public static class UserMapper
{
    public static UserDataDto MapToUserDataDto(this UserModel user)
    {
        return new UserDataDto(user.Id, user.FirstName, user.LastName, user.ColorSchema.ToString(), user.Email,
            user.ProfilePictureUrl);
    }

    public static UserModel MapToUserModel(this UserDataDto user, string id)
    {
        return new UserModel
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Email = user.Email,
            ColorSchema = Enum.Parse<ApplicationColorSchemas>(user.DataTheme)
        };
    }
}