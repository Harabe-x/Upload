using System.Runtime.InteropServices.JavaScript;
using Amazon.Runtime;
using ImageVault.ImageService.Data;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.ApiKey;
using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore;
using ApiKey = ImageVault.ImageService.Data.Dtos.ApiKey.ApiKey;

namespace ImageVault.ImageService.Repository;

public class ApiKeyRepository : IApiKeyRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public ApiKeyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<OperationResultDto<Data.Models.ApiKey>> GetKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return new OperationResultDto<Data.Models.ApiKey>(null, false, new Error("Api Key cannot be null or empty"));

        var apiKey = await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == key );

        return apiKey != null
            ? new OperationResultDto<Data.Models.ApiKey>(apiKey, true, null)
            : new OperationResultDto<Data.Models.ApiKey>(null, false, new Error("Api key not found"));
    }

    public async Task<OperationResultDto<bool>> CreateKey(ApiKey apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey.Key) || string.IsNullOrWhiteSpace(apiKey.UserId))
            return new OperationResultDto<bool>(false, false, new Error("Api key data was null or empty"));

        
        var key = new Data.Models.ApiKey()
        {
            Key = apiKey.Key,
            UserId = apiKey.UserId
        };

        await _dbContext.ApiKeys.AddAsync(key);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("Something went wrong")); 
    }
    

    public async Task<OperationResultDto<bool>> EditKey(ApiKey apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey.Key) || string.IsNullOrWhiteSpace(apiKey.UserId) || string.IsNullOrWhiteSpace(apiKey.NewKey)) 
            return new OperationResultDto<bool>(false, false, new Error("Api key data was null or empty"));
        var key = await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == apiKey.Key);
        
        if (key == null)
            return new OperationResultDto<bool>(false, false, new Error("Api key not found"));
        
        key.Key = apiKey.NewKey;

        _dbContext.ApiKeys.Update(key);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("Something went wrong."));
    }

    public async Task<OperationResultDto<bool>> DeleteKey(ApiKey apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey.Key) || string.IsNullOrWhiteSpace(apiKey.UserId))
            return new OperationResultDto<bool>(false, false, new Error("Api key data was null or empty"));
        var key = await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == apiKey.Key);

        if (key == null)
            return new OperationResultDto<bool>(false, false, new Error("Api key not found"));
       
        _dbContext.ApiKeys.Remove(key);
        
        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("Something went wrong."));
    }


    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}