namespace ImageVault.ImageService.Data.Dtos.Image;

/// <summary>
/// Record representing the data required to retrieve a specific image from a collection.
/// </summary>
/// <param name="ApiKey">The API key used for authentication.</param>
/// <param name="CollectionName">The name of the collection from which the image will be retrieved.</param>
/// <param name="ImageKey">The unique key identifying the image to be retrieved.</param>
public record GetImageDto(string ApiKey, string CollectionName, string ImageKey);
