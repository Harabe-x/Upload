using ImageVault.ClassLibrary.Validation.Interfaces;
using ImageVault.Server.Data.Dtos.UserDtos;
using ImageVault.Server.Data.Interfaces.User;
using ImageVault.Server.Data.Mappers;
using ImageVault.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace ImageVault.Server.Repository;

public class UserRepository : IUserRepository 
{

    public UserRepository(IDataValidator dataValidator ,  UserManager<ApplicationUser> userManager)
    {
        _dataValidator = dataValidator;
        _userManager = userManager;
    }
    
    public async Task<UserOperationResultDto> GetUser(string email)
    {

        if (await GetUserFromDatabase(email) is not { } applicationUser)
            return new UserOperationResultDto(null, false);

        var userDataDto = applicationUser.MapToUserData();
        
        return new UserOperationResultDto(userDataDto, true);
    }
    
    public async Task<UserOperationResultDto> UpdateUser(UserDataDto newUserData, string email)
    {
        
        if (!_dataValidator.ValidateData("ValidateUserDataDto", newUserData))  
            return new UserOperationResultDto(null, false);
        
        if (await GetUserFromDatabase(email) is not { } user)
            return new UserOperationResultDto(null, false);

        user.FirstName = newUserData.FirstName;
        user.LastName = newUserData.LastName;
        user.PreferedColorSchema = newUserData.DataTheme;
        user.ProfilePicture = newUserData.ProfilePicture;

        var updateResult = await _userManager.UpdateAsync(user);

        return new UserOperationResultDto(new UserDataDto(user.FirstName,user.LastName,user.PreferedColorSchema,user.Email,user.ProfilePicture), updateResult.Succeeded);
    }
    
    public async Task<bool> DeleteUser(string email)
    {
        if (await GetUserFromDatabase(email) is not { } user)
            return false;

        var deleteResult = await _userManager.DeleteAsync(user);

        return deleteResult.Succeeded;
    }

    private async Task<ApplicationUser> GetUserFromDatabase(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return null;
        
        return await _userManager.FindByEmailAsync(email);
    }
    
    private readonly IDataValidator _dataValidator;
    
    private readonly UserManager<ApplicationUser> _userManager; 

}