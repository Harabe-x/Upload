using Microsoft.AspNetCore.Identity;

namespace ImageVault.AuthenticationService.Data.Models;

public class ApplicationUser : IdentityUser
{
    public override string Email { get; set; }

    public override string? UserName
    {
        get => Email;
        set => base.UserName = value;
    }
}