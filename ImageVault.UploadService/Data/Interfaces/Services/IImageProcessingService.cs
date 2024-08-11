namespace ImageVault.UploadService.Data.Interfaces.Services;

public interface IImageProcessingService
{
  Task<Stream> CompressImage(IFormFile file);

 bool IsFileFormatValid(IFormFile file);
}