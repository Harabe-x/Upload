using ImageVault.Server.Data.Enums;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Models;

namespace ImageVault.Server.Validation;

public class UserProfileValidation : IUserProfileValidation
{
    public bool IsNameValid(string name)
    {
        return name.Length > 3;
    }

    public bool IsPreferedColorSchemaValid(string dataTheme)
    {
        return Enum.IsDefined(typeof(ApplicationColorSchemas), dataTheme);
    }

    public bool ValidateUser(ApplicationUser user)
    {
        var validationResult = IsNameValid(user.FirstName) && IsNameValid(user.LastName) && IsPreferedColorSchemaValid(user.PreferedColorSchema);

        return validationResult;
    }
}