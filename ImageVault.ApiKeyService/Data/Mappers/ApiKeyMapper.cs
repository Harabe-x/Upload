using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Models;
using ApiKey = ImageVault.ApiKeyService.Data.Dtos.ApiKey;

namespace ImageVault.ApiKeyService.Data.Mappers;

/// <summary>
/// Provides extension methods for mapping API keys records
/// </summary>
public static class ApiKeyMapper
{
    /// <summary>
    /// Maps an <see cref="AddApiKey"/> object to a <see cref="Models.ApiKey"/> object.
    /// </summary>
    /// <param name="apiKey">The <see cref="AddApiKey"/> object to be mapped.</param>
    /// <param name="userId">The user ID to associate with the new <see cref="Models.ApiKey"/>.</param>
    /// <returns>A <see cref="Models.ApiKey"/> object with the properties copied from the <see cref="AddApiKey"/> and the provided user ID.</returns>
    public static Models.ApiKey MapToApiKey(this AddApiKey apiKey, string userId)
    {
        return new Models.ApiKey
        {
            UserId = userId,
            KeyName = apiKey.KeyName,
            StorageCapacity = apiKey.KeyCapacity
        };
    }

    /// <summary>
    /// Maps a <see cref="Models.ApiKey"/> object to an <see cref="ApiKey"/> DTO.
    /// </summary>
    /// <param name="apiKey">The <see cref="Models.ApiKey"/> object to be mapped.</param>
    /// <returns>An <see cref="ApiKey"/> DTO with properties copied from the <see cref="Models.ApiKey"/> object.</returns>
    public static ApiKey MapToApiKeyDto(this Models.ApiKey apiKey)
    {
        return new ApiKey(apiKey.UserId, apiKey.KeyName, apiKey.Key, apiKey.StorageCapacity);
    }
}