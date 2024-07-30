using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Models;

namespace ImageVault.ApiKeyService.Data.Mappers;

public static class ApiKeyMapper
{
    public static ApiKey MapToApiKey(this AddApiKeyDto apiKey, string userId)
    {
        return new ApiKey
        {
            KeyName = apiKey.KeyName,
            StorageCapacity = apiKey.KeyCapacity
        }; 
    }

    public static ApiKeyDto MapToApiKeyDto(this ApiKey apiKey)
    {
        return new ApiKeyDto(apiKey.UserId, apiKey.KeyName, apiKey.Key, apiKey.StorageCapacity);
    }
}