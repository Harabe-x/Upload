namespace ImageVault.UploadService.Data.Dtos.Upload;

/// <summary>
/// 
/// </summary>
/// <param name="Image"></param>
/// <param name="CollectionName"></param>
/// <param name="Title"></param>
/// <param name="Description"></param>
/// <param name="UseCompression"></param>
public record ImageUpload(IFormFile Image, string CollectionName, string Title, string Description, bool UseCompression);