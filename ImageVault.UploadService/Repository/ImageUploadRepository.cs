using System.IO.Enumeration;
using System.Text;
using System.Text.Json;
using Amazon.S3;
using Amazon.S3.Model;
using ImageVault.UploadService.Data.Dtos;
using ImageVault.UploadService.Data.Dtos.ApiKey;
using ImageVault.UploadService.Data.Dtos.Upload;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.RabbitMq;
using ImageVault.UploadService.Data.Interfaces.Services;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.Data.Models;
using ImageVault.UploadService.Extension;
using ApiKey = ImageVault.UploadService.Data.Models.ApiKey;

namespace ImageVault.UploadService.Repository;


/// <summary>
///  <inheritdoc cref="IImageUploadRepository"/>
/// </summary>
public class ImageUploadRepository : IImageUploadRepository
{
    private readonly IConfiguration _configuration;

    private readonly IApiKeyRepository _apiKeyRepository; 
    
    private readonly IImageProcessingService _imageProcessingService;
    
    private readonly ILogger<ImageUploadRepository> _logger;
    
    private readonly IRabbitMqMessageSender _rabbitmqMessageSender;

    private readonly IAmazonS3Connection _s3Connection;

    public ImageUploadRepository(IAmazonS3Connection s3Connection, IConfiguration configuration,
        ILogger<ImageUploadRepository> logger,
        IRabbitMqMessageSender rabbitmqMessageSender, IImageProcessingService imageProcessingService,IApiKeyRepository apiKeyRepository)
    { 
        _configuration = configuration;
        _s3Connection = s3Connection;
        _logger = logger;
        _rabbitmqMessageSender = rabbitmqMessageSender;
        _imageProcessingService = imageProcessingService;
        _apiKeyRepository = apiKeyRepository; 
    }
    
    public async Task<OperationResultDto<ImageUploadResult>> UploadImage(ImageUploadData imageToUploadData)
    {
        if (!IsDataValid(imageToUploadData, out var error))
            return new OperationResultDto<ImageUploadResult>(null, false, error);
        
        try
        {
            var apiKey = await GetApiKey(imageToUploadData.ApiKey);

            if (apiKey == null)
                return new OperationResultDto<ImageUploadResult>(null, false, new Error("ApiKey does not exists."));

            if (!_imageProcessingService.IsFileFormatValid(imageToUploadData.Image))
                return new OperationResultDto<ImageUploadResult>(null, false,
                    new Error("Currently we don't accept file "));

            var verdict = await _apiKeyRepository.CheckIfUserCanUploadPhoto(apiKey.Key, imageToUploadData.Image.Length);

            if (!verdict.IsSuccess) return new OperationResultDto<ImageUploadResult>(null, false, new Error("")); 

            var request = await CreatePutObjectRequest(imageToUploadData, apiKey.UserId);

            await _s3Connection.S3Client.PutObjectAsync(request);

            
            
            if (!ulong.TryParse(request.Metadata["Size"], out var imageSize))
                _logger.LogCritical($"Parsing file size from metadata failed in {typeof(ImageUploadRepository)}");

            _apiKeyRepository.AddUsage(apiKey.Key, imageSize);
            
            var fileFormat = imageToUploadData.UseCompression ? ".webp" : _imageProcessingService.GetFileFormat(imageToUploadData.Image);
            
            SendRabbitMqMessages(apiKey.UserId, imageSize, apiKey.Key,request.Key,imageToUploadData.CollectionName,imageToUploadData.Title,imageToUploadData.Description,fileFormat);

            return new OperationResultDto<ImageUploadResult>(
                CreateImageUploadResult(request, imageToUploadData, imageSize), true, null);
        }
        catch (AmazonS3Exception e)
        {
            _logger.LogCritical(
                $"S3Client thrown exception | Status Code : {e.StatusCode} | Message : {e.Message} | Source : {e.Source} | S");
            return new OperationResultDto<ImageUploadResult>(null, false,
                new Error("Unknown error occured, we will fix it as soon as possible"));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected error occurred while uploading image. Message: {ex.Message}");
            return new OperationResultDto<ImageUploadResult>(null, false, new Error("Unexpected error occurred."));
        }
    }

    private async Task<ApiKey?> GetApiKey(string key)
    {
        var result = await _apiKeyRepository.GetKey(key);
        
        return result.Value;
    }
    
    private void SendRabbitMqMessages(string userId, ulong imageSize, string apiKey,string imageKey, string imageCollection, string imageTitle,string imageDescription,string fileFormat )
    {
        var apiKeyUsage = new ApiKeyUsageDto(userId, imageSize, apiKey);
        var imageData = new ImageData(imageKey, apiKey, imageCollection, imageTitle, imageDescription,userId, imageSize,fileFormat);

        _rabbitmqMessageSender.SendMessage(apiKeyUsage, _configuration.GetApiKeyUsageQueue());
        _rabbitmqMessageSender.SendMessage(imageData, _configuration.GetImageQueueName());
    }


    private async Task<PutObjectRequest> CreatePutObjectRequest(ImageUploadData imageToUploadData, string userId)
    {
        var stream = imageToUploadData.UseCompression
            ? await _imageProcessingService.CompressImage(imageToUploadData.Image)
            : imageToUploadData.Image.OpenReadStream();

        var streamLenght = stream.Length;

        var request = new PutObjectRequest
        {
            BucketName = _configuration.GetS3BucketName(),
            InputStream = stream,
            Key = CreateFileKey()
        };

        request.Metadata.Add("Title", imageToUploadData.Title);
        request.Metadata.Add("Description", imageToUploadData.Description);
        request.Metadata.Add("CollectionName", imageToUploadData.CollectionName);
        request.Metadata.Add("Owner", userId);
        request.Metadata.Add("Size", streamLenght.ToString());

        return request;
    }

    private static string CreateFileKey()
    {
        return Guid.NewGuid().ToString(); 
    }

    private static ImageUploadResult CreateImageUploadResult(PutObjectRequest request, ImageUploadData imageData, ulong imageSize)
    {
        return new ImageUploadResult(request.Key, true, DateTime.Now, imageSize + " Bytes", imageData.Title, imageData.Description, imageData.UseCompression);
    }

    private static bool IsDataValid(ImageUploadData data, out Error error)
    {
        var stringBuilder = new StringBuilder();

        if (data.Image.Length == 0) stringBuilder.AppendLine("You need to upload an image");

        if (string.IsNullOrWhiteSpace(data.ApiKey)) stringBuilder.AppendLine("Api key can't be null or empty");
        
        error = new Error(stringBuilder.ToString());

        return stringBuilder.ToString() == string.Empty; 
    }
}