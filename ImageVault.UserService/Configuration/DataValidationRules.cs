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


        validationService.AddCustomValidationRule<string?>("ValidateName",
            str => { return str != null && str.All(chr => char.IsLetter(chr)); });
        validationService.AddCustomValidationRule<string>("ValidateApplicationColorSchema",
            str => Enum.IsDefined(typeof(ApplicationColorSchemas), str.ToString().ToLower()));

        validationService.AddCustomValidationRule<UserData>("ValidateUserData", data =>
        {
            return validationService.ValidateData("ValidateName", data.FirstName)
                   && validationService.ValidateData("ValidateName", data.LastName)
                   && validationService.ValidateData("ValidateApplicationColorSchema", data.DataTheme);
        });
        
        
        validationService.AddCustomValidationRule<UpdateUser>("ValidateUserUpdateData", data =>
        {
            return validationService.ValidateData("ValidateName", data.FirstName)
                   && validationService.ValidateData("ValidateName", data.LastName)
                   && validationService.ValidateData("ValidateApplicationColorSchema", data.DataTheme);
        });
    }
}