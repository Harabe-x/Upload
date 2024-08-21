using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;

/// <summary>
///     This repository is for internal use only.
///     It should never be used in a controller
/// </summary>
public interface IAdminUserRepository
{
    /// <summary>
    ///  Gets admin user
    ///  INTERNAL USE ONLY 
    /// </summary>
    /// <returns>Admin user</returns>
    public Task<OperationResult<ApplicationUser>> GetAdminUser();
}