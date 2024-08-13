using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Models;

namespace ImageVault.AuthenticationService.Data.Interfaces.Auth;

/// <summary>
///     This interface is for internal use only.
///     It should never be used in a controller
/// </summary>
public interface IAdminUserRepository
{
    public Task<OperationResultDto<ApplicationUser>> GetAdminUser();
}