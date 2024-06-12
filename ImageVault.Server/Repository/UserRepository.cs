using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Data.Mappers;
using ImageVault.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ImageVault.Server.Repository;

public class UserRepository : IUserRepository   
{

    public UserRepository(UserManager<ApplicationUser> userManager,ILogger<UserRepository> logger)
    {
        _logger = logger; 
        _userManager = userManager;
    }

    public async Task<bool> CreateAccount(UserAccountDto accountDto)
    {
        var user = accountDto.MapUser();

        var identityResult = await _userManager.CreateAsync(user, accountDto.Password);
            
        
        if (!identityResult.Succeeded)
        {
            _logger.LogWarning($"User registration data validation failed \nError:{string.Join("\n", identityResult.Errors.Select(x => x.Code))} ");

            return false;
        }
       
        var addingRoleResult = await  _userManager.AddToRoleAsync(user,"User");

        return addingRoleResult.Succeeded;
    }

    private readonly ILogger<UserRepository> _logger;

    private readonly UserManager<ApplicationUser> _userManager; 
}