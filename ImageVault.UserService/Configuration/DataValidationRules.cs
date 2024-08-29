using System;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Enums;
using ImageVault.UserService.Data.Interfaces.Services;

namespace ImageVault.UserService.Configuration;

/// <summary>
/// A class that adds validation rules to the validator
/// </summary>
public static class DataValidationRules
{
    /// <summary>
    ///  Adds defined validation rules to the DataValidator
    /// </summary>
    /// <param name="validationService"></param>
    public static void AddRules(IDataValidationService validationService)
    {
        validationService.AddCustomValidationRule<string>("ValidateName", str => { return str.All(char.IsLetter); });
        validationService.AddCustomValidationRule<string>("ValidateApplicationColorSchema",
            str => Enum.IsDefined(typeof(ApplicationColorSchemas), str));

        validationService.AddCustomValidationRule<string>("ValidatePassword", str =>
        {
            return str.Length > 7 && str.Any(chr => !char.IsLetterOrDigit(chr)) &&
                   str.Any(chr => char.IsDigit(chr));
        });

        validationService.AddCustomValidationRule<UserData>("ValidateUserDataDto", userData =>
        {
            return validationService.ValidateData("ValidateName", userData.FirstName)
                   && validationService.ValidateData("ValidateName", userData.LastName)
                   && validationService.ValidateData("ValidateApplicationColorSchema", userData.DataTheme);
        });

        validationService.AddCustomValidationRule<string>("ValidateApiKeyName",
            apiKeyName => { return !string.IsNullOrEmpty(apiKeyName) && apiKeyName.Length > 0; });

        validationService.AddCustomValidationRule<decimal>("ValidateKeyStorageCapacity", storage =>
        {
            const decimal maxStorageCapacity = 10000;
            return storage > 0 && storage < maxStorageCapacity;
        });
    }
}