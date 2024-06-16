using System.Security.Claims;

namespace ImageVault.Server.Data.Dtos;

public record TokenValidationDto
(
    ClaimsPrincipal Claims,
    bool IsSuccess
);