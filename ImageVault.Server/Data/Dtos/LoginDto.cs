using System.ComponentModel.DataAnnotations;

namespace ImageVault.Server.Data.Dtos;

public class LoginDto
{   
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}