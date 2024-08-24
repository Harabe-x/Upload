using System.Formats.Tar;
using ImageVault.UserService.Data;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Enums;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Data.Interfaces.Services;
using ImageVault.UserService.Data.Mappers;
using ImageVault.UserService.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.UserService.Repository;

/// <summary>
///  <inheritdoc cref="IUserRepository"/>
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IDataValidationService _validationService;
    
    public UserRepository(IDataValidationService validationService, ApplicationDbContext dbContext)
    {
        _validationService = validationService;
        _dbContext = dbContext;
    }

    public async Task<OperationResultDto<UserData>> GetUser(string id)
    {
        var user = await GetUserFromDatabase(id);
 
        if (user == null)
            return new OperationResultDto<UserData>(null, false, new Error("User not found"));

        var userDataDto = user.MapToUserDataDto();

        return new OperationResultDto<UserData>(userDataDto, true, null);
    }

    public async Task<OperationResultDto<UserData>> AddUser([FromBody] UserData userData, string id)
    {
        if (!_validationService.ValidateData("ValidateUserDataDto", userData))
            return new OperationResultDto<UserData>
                (null, false, new Error("Data validation failed"));

        var user = await GetUserFromDatabase(id); 
        
        if (user != null)
            return new OperationResultDto<UserData>
                (null, false, new Error("User already exist"));

        await _dbContext.ApplicationUsers.AddAsync(userData.MapToUserModel(id));

        return await SaveChanges()
            ? new OperationResultDto<UserData>(userData, true, null)
            : new OperationResultDto<UserData>(null, false, new Error("Unexpected error occured"));
    }
    
    public async Task<OperationResultDto<UserData>> UpdateUser(UpdateUser newUserData, string id)
    {
        if (!_validationService.ValidateData("ValidateUserDataDto", newUserData))
            return new OperationResultDto<UserData>(null, false, new Error("Data validation failed"));

        var user = await GetUserFromDatabase(id); 
        
        if (user == null)
            return new OperationResultDto<UserData>(null, false, new Error("User not found"));

        user.FirstName = newUserData.FirstName;
        user.LastName = newUserData.LastName;
        user.ColorSchema = Enum.Parse<ApplicationColorSchemas>(newUserData.DataTheme);
        user.ProfilePictureUrl = newUserData.ProfilePictureUrl;

        _dbContext.Update(user);

        return await SaveChanges()
            ? new OperationResultDto<UserData>(user.MapToUserDataDto(), true, null)
            : new OperationResultDto<UserData>(null, false, new Error("Unexpected error occured"));
    }

    public async Task<bool> DeleteUser(string id)
    {

        var user = await GetUserFromDatabase(id);  
        
        if ( user == null) return false;

        _dbContext.ApplicationUsers.Remove(user);

        return await SaveChanges();
    }

    private async Task<UserModel?> GetUserFromDatabase(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;

        return await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}