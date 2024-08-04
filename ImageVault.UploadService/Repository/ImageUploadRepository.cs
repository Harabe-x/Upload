using Amazon.S3;
using Amazon.S3.Model;
using ImageVault.UploadService.Data.Dtos;
using ImageVault.UploadService.Data.Dtos.Upload;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.Data.Models;
using ImageVault.UploadService.Extension;

namespace ImageVault.UploadService.Repository;

public class ImageUploadRepository : IImageUploadRepository
{
    private readonly ILogger<ImageUploadRepository> _logger;

    private readonly IAmazonS3Connection _s3Connection;

    private readonly IConfiguration _configuration; 
    
    public ImageUploadRepository(IAmazonS3Connection s3Connection,IConfiguration configuration, ILogger<ImageUploadRepository> logger)
    {
        _configuration = configuration; 
        _s3Connection = s3Connection;
        _logger = logger; 
    }
    
    public async Task<OperationResultDto<ImageUploadResult>> UploadImage(ImageUploadData imageToUploadData , string userId)
    {
        try
        {
            var request = CreatePutObjectRequest(imageToUploadData, userId); 
            
            await _s3Connection.S3Client.PutObjectAsync(request);
            
            return new OperationResultDto<ImageUploadResult>(CreateImageUploadResult(request,imageToUploadData),true,null);
        }
        catch (AmazonS3Exception e)
        {
            _logger.LogCritical($"S3Client thrown exception | Status Code : {e.StatusCode } | Message : {e.Message} | Source : {e.Source} | S");
            return new OperationResultDto<ImageUploadResult>(null , false , new Error("Unknown error occured, we will fix it as soon as possible"));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error occurred while uploading image. Message: {ex.Message}");
            return new OperationResultDto<ImageUploadResult>(null, false, new Error("Unexpected error occurred."));
        }
    }

    private  PutObjectRequest CreatePutObjectRequest(ImageUploadData imageToUploadData,string userId)
    {
        var request =  new PutObjectRequest
        {
            BucketName = _configuration.GetS3BucketName(),
            InputStream = imageToUploadData.Image.OpenReadStream(),
        };
        
        request.Metadata.Add("Title" , imageToUploadData.Title);
        request.Metadata.Add("Description" ,  imageToUploadData.Description);
        request.Metadata.Add("CollectionName" ,  imageToUploadData.CollectionName);
        request.Metadata.Add("Owner" , userId);
        
        return request; 
    }

    private static string CreateFileKey(string userId, string collectionName, string apiKey)
    {
        return string.IsNullOrWhiteSpace(collectionName)
            ? $"{userId}/{apiKey}/{Guid.NewGuid()}"
            : $"{userId}/{apiKey}/{collectionName}/{Guid.NewGuid()}";
    }

    private static ImageUploadResult CreateImageUploadResult(PutObjectRequest request, ImageUploadData imageData)
    {
     return new ImageUploadResult(request.Key, true, DateTime.Now, imageData.Image.Length + " Bytes",
            imageData.Title, imageData.Description, imageData.UseCompression);
    }
}