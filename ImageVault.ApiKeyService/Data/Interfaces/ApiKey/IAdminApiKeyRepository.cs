using ImageVault.ApiKeyService.Data.Dtos;

namespace ImageVault.ApiKeyService.Data.Interfaces.ApiKey;

/// <summary>
/// Represents a repository for managing API keys used by other services.
/// </summary>
public interface IAdminApiKeyRepository
{
    /// <summary>
    /// Retrieves an API key associated with the specified key string.
    /// </summary>
    /// <param name="apiKey">The API key string to look up.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API key details if found, or null if not found.</returns>
    Task<Models.ApiKey> GetApiKey(string apiKey);

    /// <summary>
    /// Adds usage information to the specified API key.
    /// </summary>
    /// <param name="apiKeyUsage">The usage information to be added to the API key.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResultDto{T}"/> indicating the success or failure of the operation.</returns>
    Task<OperationResultDto<bool>> AddUsageToTheApiKey(ApiKeyUsage apiKeyUsage);
}
