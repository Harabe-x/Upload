using ImageVault.ApiKeyService.Services;

namespace ImageVault.ApiKeyService.Configuration;

public static class ValidationRules
{
    public static void AddValidatioRules(this DataValidationService validationService)
    {
        validationService.AddCustomValidationRule<string>("ValidateKeyName",
            s => { return !string.IsNullOrEmpty(s) && s.Length > 3; });

        validationService.AddCustomValidationRule<uint>("ValidateKeyCapacity", capacity => capacity > 0);
    }
}