namespace ImageVault.ImageService.Data.Dtos.Image;

/// <summary>
/// Represents the data of an image returned to the user
/// </summary>
/// <param name="Key">The unique identifier for the image.</param>
/// <param name="ImageUrl">The URL where the image can be accessed.</param>
/// <param name="CollectionName">The name of the collection to which the image belongs.</param>
/// <param name="Title">The title of the image.</param>
/// <param name="Description">The description of the image.</param>
/// <param name="ImageFormat">The format of the image (e.g., JPEG, PNG).</param>
/// <param name="CreatedAt">The date and time when the image was created.</param>
public record ImageDto(string Key, string ImageUrl, string CollectionName, string Title, string Description, string ImageFormat, DateTime CreatedAt);
