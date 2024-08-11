using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Models;

namespace ImageVault.ApiKeyService.Data.Interfaces.ApiKey;

public interface IAdminApiKeyRepository
{
    Task<Models.ApiKey> GetApiKey(string apiKey);

    Task<OperationResultDto<bool>> AddUsageToTheApiKey(ApiKeyUsageDto apiKeyUsageDto);
}