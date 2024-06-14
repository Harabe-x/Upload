using System.Security.Claims;

namespace ImageVault.Server.ExtensionMethods;

public static class ClaimValueDumper
{
        public static string? GetClaimValue(this ClaimsPrincipal claimsPrincipal,string type)
        {
            return claimsPrincipal.FindFirst(type)?.Value;
        }
}