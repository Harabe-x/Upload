using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Collection;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Models;

namespace ImageVault.ImageService.Data.Interfaces.Image;

/// <summary>
/// Interface for managing images and image collections in the database.
/// </summary>
public interface IImageManagerRepository
{
    /// <summary>
    /// Adds a new image to the database.
    /// </summary>
    /// <param name="imageData">The data of the image to add.</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the image was successfully added.
    /// </returns>
    Task<OperationResultDto<bool>> AddImage(ImageDataDto imageData);

    /// <summary>
    /// Retrieves a specific image from the database.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="imageKey">The unique key identifying the image.</param>
    /// <param name="collectionName">The name of the collection where the image is stored. Defaults to "default".</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing the requested image.
    /// </returns>
    Task<OperationResultDto<ImageDto>> GetImage(string apiKey, string imageKey, string collectionName = "default");

    /// <summary>
    /// Retrieves all images from a specified collection.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="collectionName">The name of the collection to retrieve images from. Defaults to "default".</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing an enumerable of images from the specified collection.
    /// </returns>
    Task<OperationResultDto<IEnumerable<ImageDto>>> GetImages(string apiKey, string collectionName = "default");

    /// <summary>
    /// Retrieves a paginated list of images from a specified collection.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="limit">The number of images to retrieve per page.</param>
    /// <param name="collectionName">The name of the collection to retrieve images from. Defaults to "default".</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing an enumerable of images for the specified page.
    /// </returns>
    Task<OperationResultDto<IEnumerable<ImageDto>>> GetPagedImages(string apiKey, int page, int limit, string collectionName = "default");

    /// <summary>
    /// Edits the metadata of an existing image in the database.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="imageKey">The unique key identifying the image.</param>
    /// <param name="newImageTitle">The new title of the image.</param>
    /// <param name="newImageDescription">The new description of the image.</param>
    /// <param name="collectionName">The name of the collection where the image is stored. Defaults to "default".</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the image was successfully edited.
    /// </returns>
    Task<OperationResultDto<bool>> EditImage(string apiKey, string imageKey, string newImageTitle, string newImageDescription, string collectionName = "default");

    /// <summary>
    /// Deletes an image from the database.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="imageKey">The unique key identifying the image.</param>
    /// <param name="collectionName">The name of the collection where the image is stored. Defaults to "default".</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the image was successfully deleted.
    /// </returns>
    Task<OperationResultDto<bool>> DeleteImage(string apiKey, string imageKey, string collectionName = "default");

    /// <summary>
    /// Lists all image collections in the database.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing an enumerable of all image collections.
    /// </returns>
    Task<OperationResultDto<IEnumerable<ImageCollectionDto>>> ListCollections(string apiKey);

    /// <summary>
    /// Creates a new image collection in the database.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="collectionName">The name of the collection to create.</param>
    /// <param name="description">An optional description for the new collection.</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing the created image collection.
    /// </returns>
    Task<OperationResultDto<ImageCollection>> CreateCollection(string apiKey, string collectionName, string? description = default);

    /// <summary>
    /// Edits an existing image collection in the database.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="collectionName">The name of the collec0tion to edit.</param>
    /// <param name="newCollectionName">The new name of the collection.</param>
    /// <param name="newCollectionDescription">An optional new description for the collection.</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the collection was successfully edited.
    /// </returns>
    Task<OperationResultDto<bool>> EditCollection(string apiKey, string collectionName, string newCollectionName, string? newCollectionDescription = default);

    /// <summary>
    /// Deletes an image collection from the database.
    /// </summary>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <param name="collectionName">The name of the collection to delete.</param>
    /// <returns>
    /// An <see cref="OperationResultDto{T}"/> containing a boolean value indicating whether the collection was successfully deleted.
    /// </returns>
    Task<OperationResultDto<bool>> DeleteCollection(string apiKey, string collectionName);
}
