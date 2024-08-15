using System.Reflection.Metadata.Ecma335;
using ImageVault.ImageService.Data;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        var collection = await _dbContext.ImageCollections.FirstOrDefaultAsync(x => x.CollectionName == collectionName && apiKey == x.ApiKey);

        if (collection == null && ! await CheckIfCollectionExists(apiKey,collectionName))
        {
            var result = await CreateCollection(apiKey, collectionName);
            collection = result.Value;
        }

        var image = new Image
        {
            Collection = collection.CollectionName,
            Title = title, 
            Description = description ,
            ImageCollectionId = collection.Id,
            Key = collection.ApiKey
        };
        
        collection.CollectionImages.Add(image);

        return await SaveChanges()
            ? new OperationResultDto<bool>(true,true,null)
            : new OperationResultDto<bool>(false,false,new Error("An error occurred while adding the image"));
    }

    public Task<OperationResultDto<ImageDto>> GetImage(string imageKey, string apiKey, string collectionName = "default")
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ImageDto>> GetImages(string apiKey, string collectionName = "default")
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<bool>> DeleteImage(string apiKey, string imageKey, string collectionName = "default")
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<bool>> EditImage(string apiKey, string imageKey, string newImageTitle, string newImageDescription,
        string collectionName = "default")
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<ImageCollection>> CreateCollection(string apiKey, string collectionName, string? description = default)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<bool>> EditCollection(string apiKey, string collectionName, string? description = default)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResultDto<bool>> DeleteCollection(string apiKey, string collectionName)
    {
        throw new NotImplementedException();
    }

    private async Task<ImageCollection> GetDefaultCollection()
    {
        throw new NotImplementedException();
    }

    private async Task<ImageCollection> GetCollection()
    {
        throw new NotImplementedException();
    }

    private async Task<bool> CheckIfCollectionExists(string apiKey, string collectionName)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }

}