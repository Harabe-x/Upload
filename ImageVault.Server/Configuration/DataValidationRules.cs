using ImageVault.ClassLibrary.Validation.Classes;
using ImageVault.ClassLibrary.Validation.Interfaces;
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
    }
}