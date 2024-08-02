namespace ImageVault.UploadService.Data.Dtos.Upload;

public record ImageEditResultDto(string ImageId, DateTime EditedAt, string  Title, string Description);