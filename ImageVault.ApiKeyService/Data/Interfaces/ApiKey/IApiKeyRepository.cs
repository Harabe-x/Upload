using ImageVault.ApiKeyService.Data.Dtos;

namespace ImageVault.ApiKeyService.Data.Interfaces.ApiKey;

public interface IApiKeyRepository
{
  Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData,string userId);
  
  Task<OperationResultDto<IEnumerable<ApiKeyDto>>> GetKeys(string userId);
  
  Task<OperationResultDto<ApiKeyDto>> GetKey(string key,string userId);
  
  Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto newApiKeyData, string userId,string key);

  Task<OperationResultDto<bool>> DeleteKey(string key, string userId);
  

}