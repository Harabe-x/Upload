using ImageVault.Server.Data;
using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Data.Mappers;
using ImageVault.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace ImageVault.Server.Repository;

public class UserRepository : IUserRepository 
{
    
    public UserRepository(UserManager<ApplicationUser> userManager,IUserProfileValidation userProfileValidation)
    {
        _userManager = userManager;
        _userProfileValidation = userProfileValidation; 
    }
    
    public async Task<UserProfileDto> GetUserData(string email)
    {
        var userProfile = await _userManager.FindByEmailAsync(email);

        if (userProfile == null) return null;   

        return userProfile.MapToUserProfileDto();
    }

    public async Task<bool> UpdateUserData(UpdateUserProfileDto newProfileData , string email )
    {
        var userProfile = await _userManager.FindByEmailAsync(email);

        if (userProfile == null) return false; 
        
        
        
        userProfile.FirstName = newProfileData.FirstName;
        userProfile.LastName = newProfileData.LastName;
        userProfile.PreferedColorSchema = newProfileData.PreferedColorScheme;

        if (!_userProfileValidation.ValidateUser(userProfile)) return false;

        var operationStatus = await _userManager.UpdateAsync(userProfile);

        return operationStatus.Succeeded;

    }

    public async Task<bool>  DeleteUser(string email)
    {
        var userProfile = await _userManager.FindByEmailAsync(email);

        if (userProfile == null) return false;

        var operationStatus = await _userManager.DeleteAsync(userProfile);

        return operationStatus.Succeeded;
    }

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IUserProfileValidation _userProfileValidation; 
}