using System.ComponentModel.DataAnnotations;

namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

public record RegisterAccountDto
(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] [EmailAddress] string Email,
    [Required] string Password
);