using System.Runtime.CompilerServices;
using ImageVault.ClassLibrary.Validation.Interfaces;
using ImageVault.UserService.Data;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Enums;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Data.Mappers;
using ImageVault.UserService.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.UserService.Repository;

public class UserRepository : IUserRepository 
{

    private readonly IDataValidator _dataValidator;
    
    private readonly ApplicationDbContext _dbContext; 

    
    public UserRepository(IDataValidator dataValidator ,  ApplicationDbContext dbContext)
    {
        _dataValidator = dataValidator;
        _dbContext = dbContext;
    }

 
    public async Task<UserOperationResultDto> GetUser(string id)
    {
        if (await GetUserFromDatabase(id) is not { } userModel)
            return new UserOperationResultDto(null, false, new Error("User not found"));
    
        var userDataDto = userModel.MapToUserDataDto();

        return new UserOperationResultDto(userDataDto, true, null);
        ;
    }

    public async Task<UserOperationResultDto> AddUser([FromBody]UserDataDto userData,string id )
    {
        if (!_dataValidator.ValidateData("ValidateUserDataDto", userData))  
            return new UserOperationResultDto(null, false,new Error("Data validation failed"));

        if (await GetUserFromDatabase(id) is  { })
            return new UserOperationResultDto(null, false,new Error("User already exist"));
        
        await _dbContext.ApplicationUsers.AddAsync(userData.MapToUserModel(id));
        
        return await SaveChanges()
            ? new UserOperationResultDto(userData, true, null)
            : new UserOperationResultDto(null, false, new Error("Unexpected error occured"));
        
    }
    
    
    public async Task<UserOperationResultDto> UpdateUser(UserDataDto newUserData, string id)
    {
        
        if (!_dataValidator.ValidateData("ValidateUserDataDto", newUserData))  
            return new UserOperationResultDto(null, false,new Error("Data validation failed"));
        
        if (await GetUserFromDatabase(id) is not { } user)
            return new UserOperationResultDto(null, false,new Error("User not found"));
    
        user.FirstName = newUserData.FirstName;
        user.LastName = newUserData.LastName;
        user.ColorSchema = Enum.Parse<ApplicationColorSchemas>(newUserData.DataTheme);
        user.ProfilePictureUrl= newUserData.profilePictureUrl;
    
          _dbContext.Update(user);
          
          return await SaveChanges()
              ? new UserOperationResultDto(user.MapToUserDataDto(), true, null) 
              : new UserOperationResultDto(null, false, new Error("Unexpected error occured"));

    }
                                                              
    public async Task<bool> DeleteUser(string id)
    {
        if (await GetUserFromDatabase(id) is not { } user)
            return false;
        
        _dbContext.ApplicationUsers.Remove(user);
        
        return await  SaveChanges(); 
    }
    
    private async Task<UserModel?> GetUserFromDatabase(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        
        return await _dbContext.ApplicationUsers.FirstOrDefaultAsync( x =>  x.Id == id);
    }

    public async Task<bool> SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
        return true; 
    }
    
}