using ImageVault.ImageService.Data;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces.Image;
using ImageVault.ImageService.Data.Models;

namespace ImageVault.ImageService.Repository;

public class ImageManagerRepository : IImageManagerRepository
{

    private readonly ApplicationDbContext _dbContext; 

    public ImageManagerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; 
    }

    public Task<OperationResultDto<bool>> AddImage(string imageKey, string apiKey, string title, string description, string collectionName = "default")
    {
        throw new NotImplementedException();
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

    public Task<OperationResultDto<bool>> CreateCollection(string apiKey, string collectionName, string? description = default)
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


    private async Task<bool> CheckIfDefaultCollectionExists(string apiKey)
    {
        throw new NotImplementedException();

    }
    
    private async Task<ImageCollection> CreateDefaultCollection()
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

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }

}