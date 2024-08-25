namespace ImageVault.ImageService.Data.Dtos.Image;


/// <summary>
/// Record representing the data required to delete an image from a specified collection.
/// </summary>
/// <param name="ApiKey">The API key used for authentication.</param>
/// <param name="CollectionName">The name of the collection from which the image will be deleted.</param>
/// <param name="ImageKey">The unique key identifying the image to be deleted.</param>
public record DeleteImageDto(string ApiKey, string CollectionName, string ImageKey);
