using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using Amazon.S3.Model.Internal.MarshallTransformations;
using ImageVault.ImageService.Data;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.Data.Mappers;
using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ImageVault.ImageService.Repository;

public class ImageManagerRepository : IImageManagerRepository
{

    private readonly ApplicationDbContext _dbContext; 

    public ImageManagerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    
    public async Task<OperationResultDto<bool>> AddImage(string imageKey, string apiKey, string title, string description, string collectionName = "default")
    {
        if (string.IsNullOrWhiteSpace(imageKey) || string.IsNullOrWhiteSpace(apiKey))
            return new OperationResultDto<bool>(false, false, new Error($"{nameof(apiKey)} or {nameof(imageKey)} was null"));

        var collection = await GetCollection(apiKey, collectionName);

        var image = new Image
        {
            Collection = collection.CollectionName,
            Title = title, 
            Description = description ,
            ImageCollectionId = collection.Id,
            Key = collection.ApiKey
        };
        
        collection.ImagesCollection.Add(image);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true,true,null)
            : new OperationResultDto<bool>(false,false,new Error("An error occurred while adding the image"));
    }

    public async Task<OperationResultDto<ImageDto>> GetImage(string imageKey, string apiKey, string collectionName = "default")
    {
        if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(imageKey))
            return new OperationResultDto<ImageDto>(null, false, new Error("Image Key or Api Key was null or empty"));
        
        var collection = await GetCollection(apiKey, collectionName);
        
        var image = collection.ImagesCollection.FirstOrDefault(x => x.Key == imageKey);
        
        return image != null 
            ? new OperationResultDto<ImageDto>(image.MapToImageDto(),true,null)
            : new OperationResultDto<ImageDto>(null,false,new Error("Image does not exist"));
        
    }

    public async Task<OperationResultDto<IEnumerable<ImageDto>>> GetImages(string apiKey, string collectionName = "default")
    {
        if (string.IsNullOrWhiteSpace(apiKey)) 
            return new OperationResultDto<IEnumerable<ImageDto>>(null, false, new Error("Image Key or Api Key was null or empty"));
        
        var collection = await GetCollection(apiKey, collectionName);
        
        var images = collection.ImagesCollection.Select(x => x.MapToImageDto());        
        
        return new OperationResultDto<IEnumerable<ImageDto>>(images.AsEnumerable(), true,null);
    }

    public async Task<OperationResultDto<IEnumerable<ImageDto>>> GetPagedImages(string apiKey, int page, int limit, string collectionName = "default")
    {   
        if(string.IsNullOrWhiteSpace(apiKey))       
            return new OperationResultDto<IEnumerable<ImageDto>>(null, false, new Error("Api key can't be empty"));

        var collection = await GetCollection(apiKey,collectionName);

       var selectedImages = collection.ImagesCollection.Skip(page * limit).Take(limit).Select(x => x.MapToImageDto());

        return new OperationResultDto<IEnumerable<ImageDto>>(selectedImages, true,null);
    }

    public async Task<OperationResultDto<bool>> DeleteImage(string apiKey, string imageKey, string collectionName = "default")
    {
        if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(imageKey)) 
            return new OperationResultDto<bool>(false, false, new Error("Image Key or Api Key was null or empty"));

        var collection = await GetCollection(apiKey, collectionName);

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
        if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(imageKey)) 
            return new OperationResultDto<bool>(false, false, new Error("Image Key or Api Key was null or empty"));
        var collection = await GetCollection(apiKey, collectionName);
        
        var imageToEdit = collection.ImagesCollection.FirstOrDefault(x => x.Key == imageKey);
        
        imageToEdit.Title = newImageTitle;
        imageToEdit.Description = newImageDescription;

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("Error occurred while editing the image"));
    }

    public async Task<OperationResultDto<ImageCollection>> CreateCollection(string apiKey, string collectionName, string? description = default)
    {
        if(string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(collectionName))
            new OperationResultDto<ImageCollection>(null, false, new Error("Api key can't be empty"));

        var collection = new ImageCollection()
        {
            ApiKey = apiKey,
            CollectionName = collectionName,
            Description = description 
        };
        
        return await SaveChanges()
            ? new OperationResultDto<ImageCollection>(collection, true, null)
            : new OperationResultDto<ImageCollection>(null, false, new Error("Error occurred while creating the collection"));
    }

    public async Task<OperationResultDto<bool>> EditCollection(string apiKey, string collectionName, string newCollectionName,string? newDescription = default)
    {
        if(string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(collectionName))
            new OperationResultDto<ImageCollection>(null, false, new Error("Invalid collection name or Api Key was empty"));

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
        if(string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(collectionName))
            new OperationResultDto<ImageCollection>(null, false, new Error("Invalid collection name or Api Key was empty"));

        if (collectionName == "default")
            return new OperationResultDto<bool>(false,false,new Error("Cannot edit default image collection"));

        var collection = await GetCollection(apiKey,collectionName);

        _dbContext.ImageCollections.Remove(collection);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, new Error("An error occurred while editing the collection"));
    }
    

    private async Task<ImageCollection> GetCollection(string apiKey,string collectionName)
    {
        
        var collection = await _dbContext.ImageCollections.Include(imageCollection => imageCollection.ImagesCollection).FirstOrDefaultAsync(x => x.CollectionName == collectionName && apiKey == x.ApiKey);

        if (collection != null)
        {
            return collection;
        }

        
        if (collectionName != "default") return collection;
        
        var result = await CreateCollection(apiKey, collectionName);
        collection = result.Value;
        
        return collection;
    }

    
    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }

}