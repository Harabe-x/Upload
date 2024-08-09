using System.Net.Http.Headers;
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

namespace ImageVault.UploadService.Repository;

public class ImageUploadRepository : IImageUploadRepository
{
    private readonly ILogger<ImageUploadRepository> _logger;

    private readonly IAmazonS3Connection _s3Connection;

    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    private readonly IJwtTokenProvider _jwtTokenProvider;

    private readonly IRabbitMqMessageSender _rabbitmqMessageSender;

    private readonly IImageProcessingService _imageProcessingService; 
    
    public ImageUploadRepository(IAmazonS3Connection s3Connection,IConfiguration configuration,
        ILogger<ImageUploadRepository> logger,IJwtTokenProvider jwtTokenProvider,
        IRabbitMqMessageSender rabbitmqMessageSender, IImageProcessingService imageProcessingService)
    {
        _configuration = configuration; 
        _s3Connection = s3Connection;
        _logger = logger;
        _jwtTokenProvider = jwtTokenProvider;
        _rabbitmqMessageSender = rabbitmqMessageSender;
        _imageProcessingService = imageProcessingService; 
        _httpClient = new HttpClient();
    }
    
    public async Task<OperationResultDto<ImageUploadResult>> UploadImage(ImageUploadData imageToUploadData)
    {
        try
        {
            var apiKey = await GetApiKey(imageToUploadData.ApiKey);
            
            if(apiKey == null) return new OperationResultDto<ImageUploadResult>(null, false, new Error("ApiKey not found."));
 
            if(apiKey.storageUsed + (ulong)imageToUploadData.Image.Length > apiKey.storageCapacity)
                return new OperationResultDto<ImageUploadResult>(null, false, new Error("The API key data limit has been reached"));

            // if( _imageProcessingService.ValidateFileFormat(imageToUploadData.Image))
            //     return new OperationResultDto<ImageUploadResult>(null, false  , new Error("Currently we don't accept file ") );
            //
            //    
            var request = await CreatePutObjectRequest(imageToUploadData, apiKey.userId,imageToUploadData.ApiKey);
            
            await _s3Connection.S3Client.PutObjectAsync(request);
            
           // SendRabbitMqMessages(apiKey.userId, (ulong)imageToUploadData.Image.Length, apiKey.key);
           
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

    private async Task<ApiKeyDto?> GetApiKey(string key)
    {
        var request = CreateHttpRequest(key);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();

        try
        {
            var apiKeyDto = JsonSerializer.Deserialize<ApiKeyDto>(content);

            return apiKeyDto;
        }
        catch (ArgumentNullException e)
        {
            _logger.LogError(e.Message);
            return null;
        }
        catch (JsonException e)
        {
            _logger.LogError("Cannot parse empty json.");
            return null; 
        }
        
    }

    private HttpRequestMessage CreateHttpRequest(string key)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_configuration.GetApiKeyServiceEndpoint()),
            Content = JsonContent.Create(key)
        };

        request.Headers.Add("Authorization", "Bearer " + _jwtTokenProvider.Token);

        return request;
    }

    private void SendRabbitMqMessages(string userId, ulong imageSize, string apiKey)
    {
        var apiKeyUsage = new ApiKeyUsageDto(userId,imageSize,apiKey);
        
        _rabbitmqMessageSender.SendMessage(apiKeyUsage, _configuration.GetApiKeyUsageQueue());
        
    }


    private  async Task<PutObjectRequest> CreatePutObjectRequest(ImageUploadData imageToUploadData,string userId,string apiKey)
    {
        var stream = imageToUploadData.UseCompression ? await _imageProcessingService.CompressImage(imageToUploadData.Image) : imageToUploadData.Image.OpenReadStream();

        var request =  new PutObjectRequest
        {
            BucketName = _configuration.GetS3BucketName(),
            InputStream = stream,
            Key = CreateFileKey(userId,imageToUploadData.CollectionName,apiKey)
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