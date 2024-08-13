using ImageVault.ApiKeyService.Data;
using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.ApiKeyService.Repository;

public class AdminApiKeyRepository : IAdminApiKeyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AdminApiKeyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiKey> GetApiKey(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            return null;

        return await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == apiKey);
    }

    public async Task<OperationResultDto<bool>> AddUsageToTheApiKey(ApiKeyUsageDto apiKeyUsageDto)
    {
        var apiKey = await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == apiKeyUsageDto.key);

        if (apiKey == null)
            return new OperationResultDto<bool>(false, false,
                new Error($"ApiKey Not Found in{typeof(AdminApiKeyRepository)} this shouldn't have happened  "));

        apiKey.StorageUsed += apiKeyUsageDto.usedData;

        _dbContext.ApiKeys.Update(apiKey);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false,
                new Error($"_dbContext.SaveChanges() failed in {typeof(AdminApiKeyRepository)}"));
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}