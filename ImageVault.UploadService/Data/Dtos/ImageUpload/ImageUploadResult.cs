namespace ImageVault.UploadService.Data.Dtos.Upload;

/// <summary>
/// </summary>
/// <param name="ImageId"></param>
/// <param name="UploadedAt"></param>
/// <param name="FileSize"></param>
/// <param name="Title"></param>
/// <param name="Description"></param>
/// <param name="Compressed"></param>
public record ImageUploadResult(string ImageId, bool IsSuccess, DateTime UploadedAt, string FileSize, string Title,
    string Description, bool IsCompressed);