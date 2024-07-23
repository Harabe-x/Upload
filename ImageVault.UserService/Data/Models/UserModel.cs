using System.ComponentModel.DataAnnotations;
using ImageVault.UserService.Data.Enums;

namespace ImageVault.UserService.Data.Models;

public class UserModel
{
    [Key]
    public string Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public ApplicationColorSchemas ColorSchema { get; set; }
    
    public string? ProfilePictureUrl { get; set; }
    
    public List<ApiKey> UserApiKeys { get; set; }
}