using ImageVault.UploadService.Data.Dtos;
using ImageVault.UploadService.Data.Dtos.Upload;

namespace ImageVault.UploadService.Data.Interfaces.Upload;

/// <summary>
/// Service for handling image uploads to the S3 storage.
/// </summary>
public interface IImageUploadRepository
{
    /// <summary>
    /// Uploads an image to the S3 storage.
    /// </summary>
    /// <param name="imageToUploadData">An object containing the data of the image to be uploaded.</param>
    /// <returns>A <see cref="Task{OperationResultDto{ImageUploadResult}}"/> representing the asynchronous operation, 
    /// containing the result of the image upload operation.</returns>
    public Task<OperationResultDto<ImageUploadResult>> UploadImage(ImageUploadData imageToUploadData);
}