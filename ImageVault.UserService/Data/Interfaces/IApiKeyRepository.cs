using ImageVault.UserService.Data.Dtos;
using ImageVault.UserService.Data.Dtos.ApiKey;

namespace ImageVault.UserService.Data.Interfaces;

public interface IApiKeyRepository
{
    Task<OperationResultDto<ApiKeyDto>> GetApiKey(string key, string userId);

    Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData, string userId);

    Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto apiKeyEditData, string id);

    Task<bool> DeleteApiKey(string key, string id);

}