using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.ApiKey;
using ImageVault.ImageService.Data.Models;
using ApiKey = ImageVault.ImageService.Data.Dtos.ApiKey.ApiKey;

namespace ImageVault.ImageService.Data.Interfaces;



public interface IApiKeyRepository
{
    Task<OperationResultDto<Models.ApiKey>> GetKey(string key); 
    
    Task<OperationResultDto<bool>> CreateKey(ApiKey apiKey);

    Task<OperationResultDto<bool>> EditKey(ApiKey apiKey);

    Task<OperationResultDto<bool>> DeleteKey(ApiKey apiKey); 
}