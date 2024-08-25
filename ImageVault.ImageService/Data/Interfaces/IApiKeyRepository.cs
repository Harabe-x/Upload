using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.ApiKey;
using ImageVault.ImageService.Data.Models;
using ApiKey = ImageVault.ImageService.Data.Dtos.ApiKey.ApiKey;

namespace ImageVault.ImageService.Data.Interfaces;


    /// <summary>
    /// Interface for managing API keys in the repository.
    /// </summary>
    public interface IApiKeyRepository
    {
        /// <summary>
        /// Retrieves an API key by its unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier for the API key.</param>
        /// <returns>
        /// An <see cref="OperationResultDto{T}"/> containing the requested <see cref="Models.ApiKey"/> if found, or an error if not.
        /// </returns>
        Task<OperationResultDto<Models.ApiKey>> GetKey(string key);

        /// <summary>
        /// Creates a new API key in the repository.
        /// </summary>
        /// <param name="apiKey">The API key to create.</param>
        /// <returns>
        /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the API key was successfully created.
        /// </returns>
        Task<OperationResultDto<bool>> CreateKey(ApiKey apiKey);

        /// <summary>
        /// Edits an existing API key in the repository.
        /// </summary>
        /// <param name="apiKey">The API key with updated information.</param>
        /// <returns>
        /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the API key was successfully edited.
        /// </returns>
        Task<OperationResultDto<bool>> EditKey(ApiKey apiKey);

        /// <summary>
        /// Deletes an API key from the repository.
        /// </summary>
        /// <param name="apiKey">The API key to delete.</param>
        /// <returns>
        /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the API key was successfully deleted.
        /// </returns>
        Task<OperationResultDto<bool>> DeleteKey(ApiKey apiKey);
    }
    