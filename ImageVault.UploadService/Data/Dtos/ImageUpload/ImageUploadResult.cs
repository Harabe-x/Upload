namespace ImageVault.UploadService.Data.Dtos.Upload;

/// <summary>
///  A record representing result of uploading image 
/// </summary>
/// <param name="ImageId">Unique image identifier AKA image key</param>
/// <param name="UploadedAt">The exact time the file was uploaded</param>
/// <param name="FileSize">File size in bytes</param>
/// <param name="Title">Image title</param>
/// <param name="Description">Image Description</param>
/// <param name="IsCompressed">A flag indicating whether compression has been used in the image</param>
public record ImageUploadResult(string ImageId, bool IsSuccess, DateTime UploadedAt, string FileSize, string Title,
    string Description, bool IsCompressed);