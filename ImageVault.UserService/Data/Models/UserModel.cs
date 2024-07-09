using ImageVault.UserService.Data.Enums;

namespace ImageVault.UserService.Data.Models;

public class UserModel
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ApplicationColorSchemas ColorSchema { get; set; }
}