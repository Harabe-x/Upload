using ImageVault.UploadService.Data.Dtos;
using ImageVault.UploadService.Data.Dtos.Upload;

namespace ImageVault.UploadService.Data.Interfaces.Upload;

public interface IImageUploadRepository
{
    public Task<OperationResultDto<ImageUploadResult>> UploadImage(ImageUploadData imageToUploadData,string userId);
}