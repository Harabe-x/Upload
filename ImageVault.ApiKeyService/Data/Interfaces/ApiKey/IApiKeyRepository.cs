using ImageVault.ApiKeyService.Data.Dtos;

namespace ImageVault.ApiKeyService.Data.Interfaces.ApiKey;

public interface IApiKeyRepository
{
  Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData);
  
  Task<OperationResultDto<IEnumerable<ApiKeyDto>>> GetKeys(string userId);
  
  Task<OperationResultDto<ApiKeyDto>> GetKey(string key,string userId);
  
  Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto newApiKeyData);

  Task<OperationResultDto<ApiKeyDto>> DeleteKey(string key, string userId);
  

}