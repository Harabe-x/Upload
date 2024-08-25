namespace ImageVault.ImageService.Data.Dtos.Image;

/// <summary>
/// Record representing the data required to retrieve all images from a specified collection.
/// </summary>
/// <param name="ApiKey">The API key used for authentication.</param>
/// <param name="CollectionName">The name of the collection from which the images will be retrieved.</param>
public record GetImagesDto(string ApiKey, string CollectionName);
