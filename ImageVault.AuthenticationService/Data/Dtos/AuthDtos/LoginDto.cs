using System.ComponentModel.DataAnnotations;

namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

public record LoginDto
(
    [Required] [EmailAddress] string Email,
    [Required] string Password
);