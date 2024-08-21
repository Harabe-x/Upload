using ImageVault.AuthenticationService.Data;
using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.AuthenticationService.Repository;


/// <summary>
///  <inheritdoc cref="IAdminUserRepository"/>
/// </summary>
public class AdminUserRepository : IAdminUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AdminUserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<OperationResult<ApplicationUser>> GetAdminUser()
    {
        try
        {
            var admin = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == "admin@imagevault.com");

            return admin != null
                ? new OperationResult<ApplicationUser>(admin, true, null)
                : new OperationResult<ApplicationUser>(null, false, new Error("Admin User not found"));
        }
        catch (Exception e)
        {
            return new OperationResult<ApplicationUser>(null, false, new Error(e.Message));
        }
    }
}