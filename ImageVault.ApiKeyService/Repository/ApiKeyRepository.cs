using ImageVault.ApiKeyService.Data;
using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;

namespace ImageVault.ApiKeyService.Repository;

public class ApiKeyRepository : IApiKeyRepository
{

    private readonly ApplicationDbContext _dbContext; 

    public ApiKeyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    
    public Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<IEnumerable<ApiKeyDto>>> GetKeys(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ApiKeyDto>> GetKey(string key, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto newApiKeyData)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ApiKeyDto>> DeleteKey(string key, string userId)
    {
        throw new NotImplementedException();
    }
}