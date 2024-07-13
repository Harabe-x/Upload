namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

public record AuthenticationResultDto
(
    string Email,
    string Token
);