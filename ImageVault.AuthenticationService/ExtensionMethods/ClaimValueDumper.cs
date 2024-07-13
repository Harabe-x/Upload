using System.Security.Claims;

namespace ImageVault.AuthenticationService.ExtensionMethods;

public static class ClaimValueDumper
{
    public static string? GetClaimValue(this ClaimsPrincipal claimsPrincipal, string type)
    {
        return claimsPrincipal.FindFirst(type)?.Value;
    }
}