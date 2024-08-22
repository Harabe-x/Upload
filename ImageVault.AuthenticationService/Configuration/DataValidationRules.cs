using System.ComponentModel.DataAnnotations;
using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Enums;
using ImageVault.AuthenticationService.Data.Interfaces.Services;

namespace ImageVault.AuthenticationService.Configuration;

/// <summary>
/// A class that adds validation rules to the validator
/// </summary>
public class DataValidationRules
{
    /// <summary>
    ///  Adds defined validation rules to the DataValidator
    /// </summary>
    /// <param name="validationService"></param>
    public static void AddRules(IDataValidationService validationService)
    {

        validationService.AddCustomValidationRule<string?>("ValidateEmail",
            str => { return str != null && new EmailAddressAttribute().IsValid(str); });

        validationService.AddCustomValidationRule<string?>("ValidateName",
            str => { return str != null && str.All(chr => char.IsLetter(chr)); });
        validationService.AddCustomValidationRule<string>("ValidateApplicationColorSchema",
            str => Enum.IsDefined(typeof(ApplicationColorSchemas), str));

        validationService.AddCustomValidationRule<string?>("ValidatePassword", str =>
        {
            return str != null && str.Length > 7 && str.Any(chr => !char.IsLetterOrDigit(chr)) &&
                   str.Any(chr => char.IsDigit(chr));
        });
       
        validationService.AddCustomValidationRule<RegisterAccount>("ValidateRegisterData", registerDto =>
        {
            return validationService.ValidateData("ValidateName", registerDto.FirstName)
                   && validationService.ValidateData("ValidateName", registerDto.LastName)
                   && validationService.ValidateData("ValidatePassword", registerDto.Password)
                   && validationService.ValidateData("ValidateApplicationColorSchema", registerDto.DataTheme.ToLower());
        });

        validationService.AddCustomValidationRule<Login>("ValidateLoginData", loginData =>
        {
            return validationService.ValidateData("ValidateEmail", loginData.Email) 
                   && validationService.ValidateData("ValidatePassword", loginData.Password);
        });
    }
}