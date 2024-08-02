using ImageVault.UploadService.Data.Dtos;
using ImageVault.UploadService.Data.Dtos.Upload;

namespace ImageVault.UploadService.Data.Interfaces.Upload;

public interface IImageCollectionRepository
{
    
    public Task<OperationResultDto<string>> CreateCollection();

    public Task<OperationResultDto<string>> EditCollection();

    public Task<OperationResultDto<string>> RemoveCollection();
}