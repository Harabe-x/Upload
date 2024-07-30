using ImageVault.ApiKeyService.Data;
using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Interfaces.Services;
using ImageVault.ApiKeyService.Data.Mappers;
using ImageVault.ApiKeyService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.ApiKeyService.Repository;

public class ApiKeyRepository : IApiKeyRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IDataValidationService _validationService;

    public ApiKeyRepository(ApplicationDbContext dbContext, IDataValidationService validatioNService)
    {
        _dbContext = dbContext;
        _validationService = validatioNService;
    }

    public async Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData, string userId)
    {
        if (!_validationService.ValidateData("ValidateKeyName", apiKeyData.KeyName) ||
            _validationService.ValidateData("ValidateKeyCapacity", apiKeyData.KeyCapacity))
            return new OperationResultDto<ApiKeyDto>(null, false, new Error("Invalid key data"));

        var apiKey = apiKeyData.MapToApiKey(userId);

        await _dbContext.ApiKeys.AddAsync(apiKey);

        return await SaveChanges()
            ? new OperationResultDto<ApiKeyDto>(apiKey.MapToApiKeyDto(), true, null)
            : new OperationResultDto<ApiKeyDto>(null, false, new Error("Something went wrong ..."));
    }

    public async Task<OperationResultDto<IEnumerable<ApiKeyDto>>> GetKeys(string userId)
    {
        var result = await _dbContext.ApiKeys
            .Where(x => x.UserId == userId)
            .Select(x => x.MapToApiKeyDto())
            .ToListAsync();
        return new OperationResultDto<IEnumerable<ApiKeyDto>>(result, true, null);
    }

    public async Task<OperationResultDto<ApiKeyDto>> GetKey(string key, string userId)
    {
        var apiKey = await GetApiKey(key, userId);

        return new OperationResultDto<ApiKeyDto>(apiKey.MapToApiKeyDto(), true, null);
    }

    public async Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto newApiKeyData, string userId, string key)
    {
        if (!_validationService.ValidateData("ValidateKeyName", newApiKeyData.KeyName) ||
            _validationService.ValidateData("ValidateKeyCapacity", newApiKeyData.KeyCapacity))
            return new OperationResultDto<ApiKeyDto>(null, false, new Error("Invalid key data"));

        var apiKey = await GetApiKey(key, userId);

        apiKey.KeyName = newApiKeyData.KeyName;
        apiKey.StorageCapacity = newApiKeyData.KeyCapacity;

        _dbContext.ApiKeys.Update(apiKey);

        return await SaveChanges()
            ? new OperationResultDto<ApiKeyDto>(apiKey.MapToApiKeyDto(), true, null)
            : new OperationResultDto<ApiKeyDto>(null, false, null);
    }

    public async Task<OperationResultDto<bool>> DeleteKey(string key, string userId)
    {
        var apiKey = await GetApiKey(key, userId);

        _dbContext.ApiKeys.Remove(apiKey);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, null);
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    private async Task<ApiKey> GetApiKey(string key, string userId)
    {
        return await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == key && x.UserId == userId);
    }
}