using System.ComponentModel.DataAnnotations;

namespace ImageVault.Server.Data.Dtos;

public record RegisterAccountDto
( 
    [Required] string FirstName,
    [Required] string LastName, 
    [Required] [EmailAddress] string Email, 
    [Required] string Password
);