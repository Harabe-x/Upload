using ImageVault.ApiKeyService.Data.Dtos;

namespace ImageVault.ApiKeyService.Data.Interfaces.ApiKey;

/// <summary>
/// Represents a repository for managing API keys associated with users.
/// </summary>
public interface IApiKeyRepository
{
    /// <summary>
    /// Adds a new API key for the specified user.
    /// </summary>
    /// <param name="apiKeyData">The data for the API key to be added.</param>
    /// <param name="userId">The ID of the user for whom the API key is being added.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResultDto{T}"/> indicating the success or failure of the operation, along with the created API key details.</returns>
    Task<OperationResultDto<Dtos.ApiKey>> AddKey(AddApiKey apiKeyData, string userId);

    /// <summary>
    /// Retrieves all API keys associated with the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user whose API keys are to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResultDto{T}"/> with a collection of API keys associated with the user.</returns>
    Task<OperationResultDto<IEnumerable<Dtos.ApiKey>>> GetKeys(string userId);

    /// <summary>
    /// Retrieves a specific API key associated with the specified user.
    /// </summary>
    /// <param name="key">The API key string to be retrieved.</param>
    /// <param name="userId">The ID of the user associated with the API key.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResultDto{T}"/> with the details of the requested API key.</returns>
    Task<OperationResultDto<Dtos.ApiKey>> GetKey(string key, string userId);

    /// <summary>
    /// Edits an existing API key for the specified user.
    /// </summary>
    /// <param name="newApiKeyData">The new data to update the API key.</param>
    /// <param name="userId">The ID of the user associated with the API key.</param>
    /// <param name="key">The API key string to be updated.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResultDto{T}"/> indicating the success or failure of the operation, along with the updated API key details.</returns>
    Task<OperationResultDto<Dtos.ApiKey>> EditKey(EditApiKey newApiKeyData, string userId, string key);

    /// <summary>
    /// Deletes a specific API key associated with the specified user.
    /// </summary>
    /// <param name="key">The API key string to be deleted.</param>
    /// <param name="userId">The ID of the user associated with the API key.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="OperationResultDto{T}"/> indicating the success or failure of the operation.</returns>
    Task<OperationResultDto<bool>> DeleteKey(string key, string userId);
}
