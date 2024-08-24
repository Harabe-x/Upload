using System.Security.Claims;

namespace ImageVault.UserService.Extension;

/// <summary>
///  Class responsible for dumping specified claim value
/// </summary>
public static class ClaimValueDumper
{
    /// <summary>
    ///  Gets specified claim from ClaimsPrincipal 
    /// </summary>
    /// <param name="claimsPrincipal">Object that contains claims</param>
    /// <param name="type">Desired claim</param>
    /// <returns> Desired claim value</returns>
    public static string? GetClaimValue(this ClaimsPrincipal claimsPrincipal, string type)
    {
        return claimsPrincipal.FindFirst(type)?.Value;
    }
}