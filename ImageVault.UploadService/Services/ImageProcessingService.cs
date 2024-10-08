using ImageVault.UploadService.Data.Interfaces.Services;
using ImageVault.UploadService.Extension;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;

namespace ImageVault.UploadService.Services;

public class ImageProcessingService : IImageProcessingService
{
    private readonly IConfiguration _configuration;

    public ImageProcessingService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<Stream> CompressImage(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);

        if (fileExtension == ".webp") return file.OpenReadStream();

        using var image = await Image.LoadAsync(file.OpenReadStream());

        var convertedImageStream = new MemoryStream();

        var encoder = new WebpEncoder
        {
            Quality = 90
        };
        await image.SaveAsync(convertedImageStream, encoder);

        return convertedImageStream;
    }

    public bool IsFileFormatValid(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (fileExtension == string.Empty) return false;

        var allowedExtensions = _configuration.GetAllowedFileExtensions();

        return allowedExtensions.Any(x => x == fileExtension);
    }

    public string GetFileFormat(IFormFile file)
    {
      return Path.GetExtension(file.FileName).ToLowerInvariant();
    }
}