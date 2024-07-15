using ImageVault.UserService.Data;
using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Dtos.ApiKey;
using ImageVault.UserService.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace ImageVault.UserService.Repository;

public class ApiKeyRepository : IApiKeyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ApiKeyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public Task<OperationResultDto<ApiKeyDto>> GetApiKey(string key, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto apiKeyEditData, string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteApiKey(string key, string id)
    {
        throw new NotImplementedException();
    }
}