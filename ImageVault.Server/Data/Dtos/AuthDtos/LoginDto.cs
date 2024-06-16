using System.ComponentModel.DataAnnotations;

namespace ImageVault.Server.Data.Dtos;

public record LoginDto
(
    [Required] [EmailAddress] string Email,
    [Required] string Password
);
