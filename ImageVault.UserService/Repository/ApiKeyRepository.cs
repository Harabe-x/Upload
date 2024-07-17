using System.Reflection.Metadata.Ecma335;
using ImageVault.ClassLibrary.Validation.Interfaces;
using ImageVault.UserService.Data;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Dtos.ApiKey;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Data.Mappers;
using ImageVault.UserService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ImageVault.UserService.Repository;

public class ApiKeyRepository : IApiKeyRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IDataValidator _dataValidator; 

    public ApiKeyRepository(ApplicationDbContext dbContext, IDataValidator dataValidator)
    {
        _dbContext = dbContext;
        _dataValidator = dataValidator; 
    }

    public async Task<OperationResultDto<ApiKeyDto>> GetApiKey(string key, string userId)
    {
        var apiKey = await GetKey(key, userId); 

        if (apiKey is not { })
        {
            return new OperationResultDto<ApiKeyDto>(null, false, new Error("Api key not found"));
        }

        return new OperationResultDto<ApiKeyDto>(apiKey.MapApiKeyToApiKeyDto(), true , null);
    }
    public async Task<OperationResultDto<IEnumerable<ApiKeyDto>>> GetAllApiKeys(string userId)
    {
        var apiKeyList = await _dbContext.ApiKeys.Where(x => x.UserId == userId).ToListAsync();

        var mappedList = apiKeyList.Select(x => x.MapApiKeyToApiKeyDto()).ToList();

        return new OperationResultDto<IEnumerable<ApiKeyDto>>(mappedList, true, null);
    }

    
    public async Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData, string userId)
    {
        if(!ValidateApiKeyData(apiKeyData.Name,apiKeyData.KeyCapacity, out var message))      
            return new OperationResultDto<ApiKeyDto>(null , false ,new Error(message));
        
        var apiKey = apiKeyData.MapAddApiKeyDtoToApiKey(userId);

        await _dbContext.ApiKeys.AddAsync(apiKey);

        return await SaveChanges()
            ? new OperationResultDto<ApiKeyDto>(apiKey.MapApiKeyToApiKeyDto(), true,null)
            : new OperationResultDto<ApiKeyDto>(null, false, new Error("Unexpected Error occured"));
    }

    public async Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto apiKeyEditData, string userId)
    {
        if(!ValidateApiKeyData(apiKeyEditData.Name,apiKeyEditData.KeyCapacity, out var message))      
            return new OperationResultDto<ApiKeyDto>(null , false ,new Error(message));

        var apiKey = await GetKey(apiKeyEditData.ApiKey, userId);

        if (apiKey is not { }) return new OperationResultDto<ApiKeyDto>(null, false, new Error("Api key not found"));


        apiKey.KeyName = apiKeyEditData.Name;
        apiKey.KeyStorageCapacity = apiKeyEditData.KeyCapacity;
        
        _dbContext.ApiKeys.Update(apiKey); 

        return await SaveChanges()
            ? new OperationResultDto<ApiKeyDto>(apiKey.MapApiKeyToApiKeyDto(), true,null)
            : new OperationResultDto<ApiKeyDto>(null, false, new Error("Unexpected Error occured"));

    }

    public async Task<OperationResultDto<bool>> DeleteApiKey(string apiKey, string userId)
    {
        var key = await GetKey(apiKey, userId);

        if (key is not { })  new OperationResultDto<bool>(false, false, new Error("Api key not found"));

        _dbContext.ApiKeys.Remove(key);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("deleting key failed")); 
    }

    private async Task<ApiKey?> GetKey(string key, string userId)
    { 
      return  await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == key && x.UserId == userId);
    }

    private bool ValidateApiKeyData(string apiKeyName, decimal apiKeyCapacity, out string message)
    {
        if (!_dataValidator.ValidateData("ValidateApiKeyName", apiKeyName))
        {
            message = "Invalid api key name";
            return false;
        }

        if (!_dataValidator.ValidateData("ValidateKeyStorageCapacity", apiKeyCapacity))
        {
            message = "Invalid api key storage capacity";
            return false;
        }

        message = string.Empty;

        return true;
    }
    
    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}
