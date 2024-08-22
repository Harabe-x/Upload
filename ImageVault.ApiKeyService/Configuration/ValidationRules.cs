using ImageVault.ApiKeyService.Data.Interfaces.Services;
using ImageVault.ApiKeyService.Services;

namespace ImageVault.ApiKeyService.Configuration;


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
        validationService.AddCustomValidationRule<string>("ValidateKeyName",
            s => { return !string.IsNullOrEmpty(s) && s.Length > 3; });
        
        validationService.AddCustomValidationRule<uint>("ValidateKeyCapacity", capacity => capacity > 0);
    }
}