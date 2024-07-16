using ImageVault.UserService.Data.Dtos.ApiKey;
using ImageVault.UserService.Data.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;

namespace ImageVault.UserService.Data.Mappers;

public static class ApiKeyMapper
{
    public static ApiKey? MapAddApiKeyDtoToApiKey(this AddApiKeyDto apiKeyData, string userId)
    {
        return new ApiKey
        {
            UserId = userId, 
            KeyName = apiKeyData.Name,
            KeyStorageCapacity = apiKeyData.KeyCapacity,
        };
    }

    public static ApiKeyDto MapApiKeyToApiKeyDto(this ApiKey? apiKey)
    {
        return new ApiKeyDto(apiKey.KeyName, apiKey.Key, apiKey.KeyStorageCapacity, apiKey.CreatedAt);
    }
}