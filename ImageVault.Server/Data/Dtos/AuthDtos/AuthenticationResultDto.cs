using System.ComponentModel.DataAnnotations;

namespace ImageVault.Server.Data.Dtos;

public record AuthenticationResultDto
(
    string Name,
    string Email,
    string Token
);
