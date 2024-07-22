using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Dtos.ApiKey;

namespace ImageVault.UserService.Data.Interfaces;

public interface IApiKeyRepository
{
    Task<OperationResultDto<ApiKeyDto>> GetApiKey(string apiKey, string userId);

    Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData, string userId);

    Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto apiKeyEditData, string userId);

    Task<OperationResultDto<IEnumerable<ApiKeyDto>>> GetAllApiKeys(string userId); 

    Task<OperationResultDto<bool>> DeleteApiKey(string apiKey, string userId);
}