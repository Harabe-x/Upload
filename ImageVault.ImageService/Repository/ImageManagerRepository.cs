using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using Amazon.S3.Model;
using Amazon.S3.Model.Internal.MarshallTransformations;
using ImageVault.ImageService.Data;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.Data.Mappers;
using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using HostingEnvironmentExtensions = Microsoft.Extensions.Hosting.HostingEnvironmentExtensions;

namespace ImageVault.ImageService.Repository;

public class ImageManagerRepository : IImageManagerRepository
{
    private readonly ILogger<ImageManagerRepository> _logger; 
    
    private readonly ApplicationDbContext _dbContext;

    public ImageManagerRepository(ApplicationDbContext dbContext, ILogger<ImageManagerRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger; 
    }
    
    public async Task<OperationResultDto<bool>> AddImage(ImageDataDto imageData)
    {
        var collectionName = imageData.Collection; 
        
        if(ValidateCommonInput(imageData.ApiKey, ref collectionName,out var error,imageData.Key)) return new OperationResultDto<ImageDto>(null, false, error);

        var collection = await GetCollection(imageData.ApiKey, imageData.Collection);

        if (collection == null)
        {
            var result = await CreateCollection(imageData.ApiKey, imageData.Collection);

            if (!result.IsSuccess || result.Value == null)
                return new OperationResultDto<bool>(false, false, new Error("Cannot create collection"));
            collection = result.Value;
        }
        
        var image = imageData.MapToImage(collection);
        
        collection.ImagesCollection.Add(image);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true,true,null)
            : new OperationResultDto<bool>(false,false,new Error("An error occurred while adding the image"));
    }

    public async Task<OperationResultDto<ImageDto>> GetImage(string imageKey, string apiKey, string collectionName = "default")
    {
        if(ValidateCommonInput(apiKey, ref collectionName,out var error, imageKey)) return new OperationResultDto<ImageDto>(null, false, error);
        
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
        if(ValidateCommonInput(apiKey, ref collectionName,out var error)) return new OperationResultDto<IEnumerable<ImageDto>(null, false, error);
        
        var collection = await GetCollection(apiKey, collectionName);

        if (collection == null)
            return new OperationResultDto<IEnumerable<ImageDto>>(null, false, new Error($"Collection named {collectionName} does not exists" ));
        
        var images = collection.ImagesCollection.Select(x => x.MapToImageDto());        
        
        return new OperationResultDto<IEnumerable<ImageDto>>(images.AsEnumerable(), true,null);
    }

    public async Task<OperationResultDto<IEnumerable<ImageDto>>> GetPagedImages(string apiKey, int page, int limit, string collectionName = "default")
    {   
        if(ValidateCommonInput(apiKey, ref collectionName,out var error)) return new OperationResultDto<IEnumerable<ImageDto>(null, false, error);

        var collection = await GetCollection(apiKey,collectionName);
        
        if (collection == null)
            return new OperationResultDto<IEnumerable<ImageDto>>(null, false, new Error($"Collection named {collectionName} does not exists" ));
        
        var pageToSkip = (page - 1) * limit; 
        
       var selectedImages = collection.ImagesCollection.Skip(pageToSkip).Take(limit).Select(x => x.MapToImageDto());

        return new OperationResultDto<IEnumerable<ImageDto>>(selectedImages, true,null);
    }

    public async Task<OperationResultDto<bool>> DeleteImage(string apiKey, string imageKey, string collectionName = "default")
    {
        if(ValidateCommonInput(apiKey, ref collectionName,out var error,imageKey)) return new OperationResultDto<bool>(false, false, error);

        var collection = await GetCollection(apiKey, collectionName);
        
        if (collection == null)
            return new OperationResultDto<bool>(false, false, new Error($"Collection named {collectionName} does not exists" ));
        
        var imageToRemove = collection.ImagesCollection.FirstOrDefault(x => x.Key == imageKey);

        if (imageToRemove == null)
            return new OperationResultDto<bool>(false, false, new Error("Image does not exist")); 
        
        collection.ImagesCollection.Remove(imageToRemove);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false,false,new Error("An error occurred while deleting the image"));

    }

    public async Task<OperationResultDto<bool>> EditImage(string apiKey, string imageKey, string newImageTitle, string newImageDescription,
        string collectionName = "default")
    {
        if(ValidateCommonInput(apiKey, ref collectionName,out var error,imageKey)) return new OperationResultDto<bool>(false, false, error);

        
        var collection = await GetCollection(apiKey, collectionName);
        
        if (collection == null)
            return new OperationResultDto<bool>(false, false, new Error($"Collection named {collectionName} does not exists" ));
       
        var imageToEdit = collection.ImagesCollection.FirstOrDefault(x => x.Key == imageKey);
        
        imageToEdit.Title = newImageTitle;
        imageToEdit.Description = newImageDescription;

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("Error occurred while editing the image"));
    }

    public async Task<OperationResultDto<ImageCollection>> CreateCollection(string apiKey, string collectionName, string? description = default)
    {  
        if(ValidateCommonInput(apiKey, ref collectionName,out var error)) return new OperationResultDto<ImageCollection>(null, false, error);
        
        // TODO : I need to validate apikey somehow, i wonder if i should use amqp or http. using amqp means that i have to store copy of all ApiKeys in localDb 
        
        var collection = new ImageCollection()
        {
            ApiKey = apiKey,
            CollectionName = collectionName,
            Description = description 
        };

        await _dbContext.ImageCollections.AddAsync(collection); 
        
        return await SaveChanges()
            ? new OperationResultDto<ImageCollection>(collection, true, null)
            : new OperationResultDto<ImageCollection>(null, false, new Error("Error occurred while creating the collection"));
    }

    public async Task<OperationResultDto<bool>> EditCollection(string apiKey, string collectionName, string newCollectionName,string? newDescription = default)
    {
        if(ValidateCommonInput(apiKey, ref collectionName,out var error)) return new OperationResultDto<bool>(false, false, error);
        
        if (collectionName == "default")
            return new OperationResultDto<bool>(false,false,new Error("Cannot edit default image collection"));
        
        var collection = await GetCollection(apiKey, collectionName);
        
        collection.CollectionName = newCollectionName;
        collection.Description = newDescription;

        _dbContext.ImageCollections.Update(collection);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("An error occurred while editing the collection"));
    }

    public async Task<OperationResultDto<bool>> DeleteCollection(string apiKey, string collectionName)
    {
        if(ValidateCommonInput(apiKey, ref collectionName,out var error)) return new OperationResultDto<bool>(false, false, error);

        if (collectionName == "default")
            return new OperationResultDto<bool>(false,false,new Error("Cannot delete default image collection"));

        var collection = await GetCollection(apiKey,collectionName);

        _dbContext.ImageCollections.Remove(collection);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("An error occurred while editing the collection"));
    }


    private async Task<ImageCollection?> GetCollection(string apiKey, string collectionName = "default")
    {
        var collection = await _dbContext.ImageCollections.Include(imageCollection => imageCollection.ImagesCollection)
            .FirstOrDefaultAsync(x => x.CollectionName == collectionName && apiKey == x.ApiKey);

        return collection; 
    }

    public static bool ValidateCommonInput(string apiKey,  ref string collectionName, out Error error,string? imageKey = "Foo")
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

        error = new Error(stringBuilder.ToString());

        return stringBuilder.ToString() == string.Empty; 
    }
    
    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }

}