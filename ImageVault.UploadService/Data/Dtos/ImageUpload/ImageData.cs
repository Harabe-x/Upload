namespace ImageVault.UploadService.Data.Dtos.Upload;

/// <summary>
///  A record representing data sent to ImageService via AMQP
/// </summary>
/// <param name="Key">Unique image key</param>
/// <param name="ApiKey">API key from which the photo was sent</param>
/// <param name="Collection">A collection that contains an image</param>
/// <param name="Title">Title of upload image </param>
/// <param name="Description">Description of upload image</param>
/// <param name="UserId">The identifier to which this photo belongs</param>
/// <param name="ImageSize">Image size in bytes</param>
/// <param name="FileFormat">Format of the image </param>
public record ImageData(string Key, string ApiKey, string Collection, string Title, string Description,string UserId,ulong ImageSize,string FileFormat);
