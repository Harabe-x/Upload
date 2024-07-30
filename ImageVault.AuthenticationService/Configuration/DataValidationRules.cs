using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Dtos.UserDtos;
using ImageVault.AuthenticationService.Data.Enums;
using ImageVault.AuthenticationService.Data.Interfaces.Services;

namespace ImageVault.AuthenticationService.Configuration;

public class DataValidationRules
{
    public static void AddRules(IDataValidationService validationService)
    {
        validationService.AddCustomValidationRule<string>("ValidateName",
            str => { return str.All(chr => char.IsLetter(chr)); });
        validationService.AddCustomValidationRule<string>("ValidateApplicationColorSchema",
            str => Enum.IsDefined(typeof(ApplicationColorSchemas), str));

        validationService.AddCustomValidationRule<string>("ValidatePassword", str =>
        {
            return str.Length > 7 && str.Any(chr => !char.IsLetterOrDigit(chr)) &&
                   str.Any(chr => char.IsDigit(chr));
        });
        validationService.AddCustomValidationRule<RegisterAccountDto>("ValidateRegisterDto", registerDto =>
        {
            return validationService.ValidateData("ValidateName", registerDto.FirstName)
                   && validationService.ValidateData("ValidateName", registerDto.LastName)
                   && validationService.ValidateData("ValidatePassword", registerDto.Password);
        });
        validationService.AddCustomValidationRule<UserDataDto>("ValidateUserDataDto", userData =>
        {
            return validationService.ValidateData("ValidateName", userData.FirstName)
                   && validationService.ValidateData("ValidateName", userData.LastName)
                   && validationService.ValidateData("ValidateApplicationColorSchema", userData.DataTheme);
        });
    }
}