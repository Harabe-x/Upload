using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.ApiKey;
using ImageVault.ImageService.Data.Models;

namespace ImageVault.ImageService.Data.Interfaces;



public interface IApiKeyRepository
{
    Task<OperationResultDto<ApiKey>> GetKey(string key); 
    
    Task<OperationResultDto<bool>> CreateKey(ApiKeyDto apiKey);

    Task<OperationResultDto<bool>> EditKey(ApiKeyDto apiKey);

    Task<OperationResultDto<bool>> DeleteKey(ApiKeyDto apiKey); 
}