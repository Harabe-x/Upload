using ImageVault.UploadService.Data.Dtos;
using ImageVault.UploadService.Data.Dtos.Upload;

namespace ImageVault.UploadService.Data.Interfaces.Upload;

public interface IImageUploadRepository
{
    public Task<OperationResultDto<ImageUploadResult>> UploadImage(ImageUpload imageToImageUpload);

    public Task<OperationResultDto<ImageRemoveResult>> RemoveImage(ImageRemoveData imageRemoveData);

    public Task<OperationResultDto<ImageEditResultDto>> EditImageDetails(ImageEditData imageEditData);
}