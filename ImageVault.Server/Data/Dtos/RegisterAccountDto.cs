using System.ComponentModel.DataAnnotations;

namespace ImageVault.Server.Data.Dtos;

public class RegisterAccountDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
 
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}