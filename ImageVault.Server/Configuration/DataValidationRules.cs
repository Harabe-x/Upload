using ImageVault.ClassLibrary.Validation.Classes;
using ImageVault.ClassLibrary.Validation.Interfaces;
using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Dtos.UserDtos;
using ImageVault.Server.Data.Enums;

namespace ImageVault.Server.Configuration;

public class DataValidationRules
{
    public static void AddRules(DataValidator validator )
    {
        validator.AddCustomValidationRule<string>("ValidateName", (str) =>
            {
                return str.All(chr => char.IsLetter(chr));
            });
            validator.AddCustomValidationRule<string>("ValidateApplicationColorSchema", (str) => Enum.IsDefined(typeof(ApplicationColorSchemas), str));
          
            validator.AddCustomValidationRule<string>("ValidatePassword", (str) =>
            {
                return str.Length > 7 && str.Any(chr => !char.IsLetterOrDigit(chr)) &&
                       str.Any(chr => char.IsDigit(chr));
            });
            validator.AddCustomValidationRule<RegisterAccountDto>("ValidateRegisterDto", (registerDto) =>
            {
                return validator.ValidateData("ValidateName", registerDto.FirstName)
                       && validator.ValidateData("ValidateName",registerDto.LastName)
                       && validator.ValidateData("ValidatePassword",registerDto.Password); 
            });
            validator.AddCustomValidationRule<UserDataDto>("ValidateUserDataDto", (userData) =>
            {
                return validator.ValidateData("ValidateName", userData.FirstName)
                       && validator.ValidateData("ValidateName", userData.LastName)
                       && validator.ValidateData("ValidateApplicationColorSchema", userData.DataTheme);
            });
    }
}