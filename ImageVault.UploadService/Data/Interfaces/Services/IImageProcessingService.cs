namespace ImageVault.UploadService.Data.Interfaces.Services;

/// <summary>
/// Interface for image processing services, providing methods for image compression
/// and validation of image formats.
/// </summary>
public interface IImageProcessingService
{
    /// <summary>
    /// Compresses the provided image file.
    /// </summary>
    /// <param name="file">The image file to be compressed.</param>
    /// <returns>A <see cref="Task{Stream}"/> representing the asynchronous operation, 
    /// containing the compressed image as a stream.</returns>
    Task<Stream> CompressImage(IFormFile file);

    /// <summary>
    /// Gets the format of the provided image file.
    /// </summary>
    /// <param name="file">The image file for which to determine the format.</param>
    /// <returns>A string representing the format of the file, such as "jpg", "png", etc.</returns>
    string GetFileFormat(IFormFile file); 
    
    /// <summary>
    /// Validates whether the provided image file has a supported format.
    /// </summary>
    /// <param name="file">The image file to be validated.</param>
    /// <returns>True if the file format is valid; otherwise, false.</returns>
    bool IsFileFormatValid(IFormFile file);
}