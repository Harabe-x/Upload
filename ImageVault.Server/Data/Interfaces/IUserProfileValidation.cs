using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Interfaces;

public interface IUserProfileValidation
{
    public  bool IsNameValid(string name);
    
    public bool IsPreferedColorSchemaValid(string dataTheme);

    public  bool ValidateUser(ApplicationUser user);
}   
