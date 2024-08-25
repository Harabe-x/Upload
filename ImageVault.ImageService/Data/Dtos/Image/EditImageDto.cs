namespace ImageVault.ImageService.Data.Dtos.Image;

/// <summary>
/// Record representing the data required to edit an existing image's metadata.
/// </summary>
/// <param name="ApiKey">The API key used for authentication.</param>
/// <param name="ImageKey">The unique key identifying the image to be edited.</param>
/// <param name="NewTitle">The new title for the image.</param>
/// <param name="NewDescription">The new description for the image.</param>
public record EditImageDto(string ApiKey, string ImageKey, string NewTitle, string NewDescription);
