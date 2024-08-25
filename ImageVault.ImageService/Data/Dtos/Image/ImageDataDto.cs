namespace ImageVault.ImageService.Data.Dtos.Image;
/// <summary>
/// Represents the necessary data to add a new photo to the database
/// </summary>
/// <param name="Key">The unique identifier for the image.</param>
/// <param name="ApiKey">The API key used for authentication.</param>
/// <param name="Collection">The name of the collection to which the image belongs.</param>
/// <param name="Title">The title of the image.</param>
/// <param name="Description">The description of the image.</param>
/// <param name="UserId">The identifier of the user who uploaded or owns the image.</param>
/// <param name="ImageSize">The size of the image in bytes.</param>
/// <param name="FileFormat">The file format of the image (e.g., JPEG, PNG).</param>
public record ImageDataDto(string Key, string ApiKey, string Collection, string Title, string Description, string UserId, ulong ImageSize, string FileFormat);
