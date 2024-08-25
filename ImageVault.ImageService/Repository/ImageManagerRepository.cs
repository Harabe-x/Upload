using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using ImageVault.ImageService.Data;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Collection;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces;
using ImageVault.ImageService.Data.Interfaces.Amazon;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.Data.Mappers;
using ImageVault.ImageService.Data.Models;
using ImageVault.ImageService.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ImageVault.ImageService.Repository;

/// <summary>
/// <inheritdoc cref="IImageManagerRepository" /> 
/// </summary>
public class ImageManagerRepository : IImageManagerRepository
{
    private readonly ILogger<ImageManagerRepository> _logger; 
    
    private readonly ApplicationDbContext _dbContext;

    private readonly IApiKeyRepository _apiKeyRepository;

    private IConfiguration _configuration;

    private IAmazonS3Connection _s3Connection; 
    
    public ImageManagerRepository(ApplicationDbContext dbContext, ILogger<ImageManagerRepository> logger,IAmazonS3Connection s3Connection, IApiKeyRepository apiKeyRepository,IConfiguration configuration)
    {
        _dbContext = dbContext;
        _logger = logger;                   
        _apiKeyRepository = apiKeyRepository;
        _configuration = configuration;
        _s3Connection = s3Connection; 
    }
    
    public async Task<OperationResultDto<bool>> AddImage(ImageDataDto imageData)
    {
        var collectionName = imageData.Collection; 
        
        if(!ValidateAndSetDefaults(imageData.ApiKey, ref collectionName,out var error,imageData.Key)) return new OperationResultDto<bool>(false, false, error);

        var  collection = await GetCollection(imageData.ApiKey, imageData.Collection);

        if (collection == null)
        {
            await CreateCollection(imageData.ApiKey, imageData.Collection);

            collection = await GetCollection(imageData.ApiKey, imageData.Collection);
            
            if (collection == null) return new OperationResultDto<bool>(false, false, new Error("Cannot create collection"));
        }
        
        var image = imageData.MapToImage(collection);
        
        collection.ImagesCollection.Add(image);

        collection.TotalImages += 1; 

        return await SaveChanges()
            ? new OperationResultDto<bool>(true,true,null)
            : new OperationResultDto<bool>(false,false,new Error("An error occurred while adding the image"));
    }

    public async Task<OperationResultDto<ImageDto>> GetImage(string apiKey, string imageKey, string collectionName = "default")
    {
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error, imageKey)) return new OperationResultDto<ImageDto>(null, false, error);
        
        var collection = await GetCollection(apiKey, collectionName);

        if (collection == null)
            return new OperationResultDto<ImageDto>(null,false,new Error($"Collection named {collectionName} does not exists"));
        
        var image = collection.ImagesCollection.FirstOrDefault(x => x.Key == imageKey);
        
        return image != null 
            ? new OperationResultDto<ImageDto>(image.MapToImageDto(),true,null)
            : new OperationResultDto<ImageDto>(null,false,new Error("Image does not exist"));
        
    }

    public async Task<OperationResultDto<IEnumerable<ImageDto>>> GetImages(string apiKey, string collectionName = "default")
    {
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error)) return new OperationResultDto<IEnumerable<ImageDto>>(null, false, error);
        
        var collection = await GetCollection(apiKey, collectionName);

        if (collection == null)
            return new OperationResultDto<IEnumerable<ImageDto>>(null, false, new Error($"Collection named {collectionName} does not exists" ));
        
        var images = collection.ImagesCollection.Select(x => x.MapToImageDto());        
        
        return new OperationResultDto<IEnumerable<ImageDto>>(images.AsEnumerable(), true,null);
    }

    public async Task<OperationResultDto<IEnumerable<ImageDto>>> GetPagedImages(string apiKey, int page, int limit, string collectionName = "default")
    {   
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error)) return new OperationResultDto<IEnumerable<ImageDto>>(null, false, error);

        var collection = await GetCollection(apiKey,collectionName);
        
        if (collection == null)
            return new OperationResultDto<IEnumerable<ImageDto>>(null, false, new Error($"Collection named {collectionName} does not exists" ));
        
        var pageToSkip = (page - 1) * limit; 
        
       var selectedImages = collection.ImagesCollection.Skip(pageToSkip).Take(limit).Select(x => x.MapToImageDto());

        return new OperationResultDto<IEnumerable<ImageDto>>(selectedImages, true,null);
    }

    public async Task<OperationResultDto<bool>> DeleteImage(string apiKey, string imageKey, string collectionName = "default")
    {
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error,imageKey)) return new OperationResultDto<bool>(false, false, error);

        var collection = await GetCollection(apiKey, collectionName);
        
        if (collection == null)
            return new OperationResultDto<bool>(false, false, new Error($"Collection named {collectionName} does not exists" ));
        
        var imageToRemove = collection.ImagesCollection.FirstOrDefault(x => x.Key == imageKey);

        if (imageToRemove == null)
            return new OperationResultDto<bool>(false, false, new Error("Image does not exist"));

        if (!await DeleteS3Object(imageToRemove.Key))
            return new OperationResultDto<bool>(false, false, new Error("Cannot delete image right now. Please try again later."));         
        
        collection.ImagesCollection.Remove(imageToRemove);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false,false,new Error("An error occurred while deleting the image"));
    }

    public async Task<OperationResultDto<IEnumerable<ImageCollectionDto>>> ListCollections(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            return new OperationResultDto<IEnumerable<ImageCollectionDto>>(null, false,
                new Error("Api key cannot be empty"));

        var collections = await _dbContext.ImageCollections
            .Where(x => x.ApiKey == apiKey)
            .Select(x => x.MapToCollectionDto())
            .ToListAsync();

        return collections != null
            ? new OperationResultDto<IEnumerable<ImageCollectionDto>>(collections, true, null)
            : new OperationResultDto<IEnumerable<ImageCollectionDto>>(null, false, new Error("Collection not found"));
    }

    public async Task<OperationResultDto<bool>> EditImage(string apiKey, string imageKey, string newImageTitle, string newImageDescription,
        string collectionName = "default")
    {
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error,imageKey)) return new OperationResultDto<bool>(false, false, error);
        
        var collection = await GetCollection(apiKey, collectionName);
        
        if (collection == null)
            return new OperationResultDto<bool>(false, false, new Error($"Collection named {collectionName} does not exists" ));
       
        var imageToEdit = collection.ImagesCollection.FirstOrDefault(x => x.Key == imageKey);

        if (imageToEdit == null)
            return new OperationResultDto<bool>(false, false, new Error("Image not found."));
        
        imageToEdit.Title = newImageTitle;
        imageToEdit.Description = newImageDescription;

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("Error occurred while editing the image"));
    }

    public async Task<OperationResultDto<ImageCollection>> CreateCollection(string apiKey, string collectionName, string? description = default)
    {  
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error)) return new OperationResultDto<ImageCollection>(null, false, error);
        
        var keySearchResult = await _apiKeyRepository.GetKey(apiKey);

        if (!keySearchResult.IsSuccess) return new OperationResultDto<ImageCollection>(null, false, keySearchResult.Error);
        
        var collection = new ImageCollection()
        {
            ApiKey = apiKey,
            CollectionName = collectionName,
            Description = description ,
            TotalImages = 0
        };

        await _dbContext.ImageCollections.AddAsync(collection); 
        
        return await SaveChanges()
            ? new OperationResultDto<ImageCollection>(collection, true, null)
            : new OperationResultDto<ImageCollection>(null, false, new Error("Error occurred while creating the collection"));
    }

    public async Task<OperationResultDto<bool>> EditCollection(string apiKey, string collectionName, string newCollectionName,string? newDescription = default)
    {
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error)) return new OperationResultDto<bool>(false, false, error);
        
        if (collectionName == "default")
            return new OperationResultDto<bool>(false,false,new Error("Cannot edit default image collection"));
        
        var collection = await GetCollection(apiKey, collectionName);

        if (collection.CollectionName == newCollectionName && collection.Description == newDescription)
            return new OperationResultDto<bool>(false,false,new Error("Data update failed. The data provided is identical to that in the database"));
        
        collection.CollectionName = newCollectionName;
        collection.Description = newDescription;

        _dbContext.ImageCollections.Update(collection);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("An error occurred while editing the collection"));
    }

    public async Task<OperationResultDto<bool>> DeleteCollection(string apiKey, string collectionName)
    {
        if(!ValidateAndSetDefaults(apiKey, ref collectionName,out var error)) return new OperationResultDto<bool>(false, false, error);

        if (collectionName == "default")
            return new OperationResultDto<bool>(false,false,new Error("Cannot delete default image collection"));

        var collection = await GetCollection(apiKey,collectionName);

        if (collection == null)
            return new OperationResultDto<bool>(false, false, new Error($"{collectionName} collection doesn't exists"));

        const int objectLimit = 1000; 
        
        // Keys are chunked because the maximum number of objects that can be deleted in one request is 1000 
        var imageKeys = collection.ImagesCollection
                .Select(x => new KeyVersion { Key = x.Key })
                .ToList()
                .Chunk(objectLimit);
        
        foreach (var keyList in imageKeys)
        {
            if (!await DeleteS3Objects(keyList))
            {
                return new OperationResultDto<bool>(false, false , new Error("The collection cannot be deleted at this time. Please try again later"));
            }
        }
        
        _dbContext.ImageCollections.Remove(collection);
        
        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("An error occurred while editing the collection"));
    }


    private async Task<ImageCollection?> GetCollection(string apiKey, string collectionName = "default")
    {
        var collection = await
            _dbContext.ImageCollections
                .Include(imageCollection => imageCollection.ImagesCollection)
                .FirstOrDefaultAsync(x => x.CollectionName == collectionName && apiKey == x.ApiKey);

        return collection; 
    }

    private static bool ValidateAndSetDefaults(string apiKey,  ref string collectionName, out Error error,string? imageKey = "Foo")
    {
        var stringBuilder = new StringBuilder();
        
        
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            stringBuilder.AppendLine("[*] Api key can't be empty");
        }

        if (string.IsNullOrWhiteSpace(imageKey))
        {
            stringBuilder.AppendLine("[*] ImageKey can't be null or empty");
        }

        if (string.IsNullOrWhiteSpace(collectionName)) collectionName = "default";

        collectionName = collectionName.ToLower(); 
        
        error = new Error(stringBuilder.ToString());

        return stringBuilder.ToString() == string.Empty; 
    }

    private async Task<bool> DeleteS3Objects(IEnumerable<KeyVersion> objectKeys)
    {
        var request = CreateDeleteObjectsRequest(objectKeys.ToList());
        try
        {
            await _s3Connection.S3Client.DeleteObjectsAsync(request);
        }
        catch (AmazonS3Exception e )
        {
            _logger.LogError(e.ToString());
            return false; 
        }

        return true; 
    }

    private async Task<bool> DeleteS3Object(string objectKey)
    {
        var request = CreateDeleteObjectRequest(objectKey);

        try
        {
            await _s3Connection.S3Client.DeleteObjectAsync(request);
        }
        catch (AmazonS3Exception e)
        {
            _logger.LogError(e.ToString());
            return false; 
        }

        return true; 
    }

    private DeleteObjectRequest CreateDeleteObjectRequest(string objectKey)
    {
        return new DeleteObjectRequest()
        {
            BucketName = _configuration.GetS3BucketName(),
            Key = objectKey
        };
    }
    
    private  DeleteObjectsRequest CreateDeleteObjectsRequest(List<KeyVersion> objectList)
    {
        return new DeleteObjectsRequest()
        {
            BucketName = _configuration.GetS3BucketName(),
            Objects = objectList
        };
    }
    
    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }

}