using Amazon.S3.Model;
using ImageVault.UploadService.Data.Dtos;
using ImageVault.UploadService.Data.Dtos.Upload;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.Extension;

namespace ImageVault.UploadService.Repository;

public class ImageUploadRepository : IImageUploadRepository
{

    private readonly IAmazonS3Connection _s3Connection;

    private readonly IConfiguration _configuration; 
    
    public ImageUploadRepository(IAmazonS3Connection s3Connection,IConfiguration configuration)
    {
        _configuration = configuration; 
        _s3Connection = s3Connection; 
    }
    
    public async Task<OperationResultDto<ImageUploadResult>> UploadImage(ImageUpload imageToImageUpload)
    {


        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _configuration.GetS3BucketName(),
            InputStream = imageToImageUpload.Image.OpenReadStream(),
            Key = Guid.NewGuid().ToString()
        };


        await _s3Connection.S3Client.PutObjectAsync(putObjectRequest);

        return new OperationResultDto<ImageUploadResult>(new ImageUploadResult(null,false ,DateTime.Now ,null,null,null,false),true,null);
    }

    public Task<OperationResultDto<ImageRemoveResult>> RemoveImage(ImageRemoveData imageRemoveData)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ImageEditResultDto>> EditImageDetails(ImageEditData imageEditData)
    {
        throw new NotImplementedException();
    }
}