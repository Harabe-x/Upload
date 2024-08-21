using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Collection;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Models;

namespace ImageVault.ImageService.Data.Interfaces.Image;

public interface IImageManagerRepository
{
    Task<OperationResultDto<bool>> AddImage(ImageDataDto imageData);
    
    Task<OperationResultDto<ImageDto>> GetImage(string apiKey,string imageKey, string collectionName = "default");
    
    Task<OperationResultDto<IEnumerable<ImageDto>>> GetImages(string apiKey,string collectionName = "default");
    
    Task<OperationResultDto<IEnumerable<ImageDto>>> GetPagedImages(string apiKey,int page,int limit, string collectionName = "default"); 
    
    Task<OperationResultDto<bool>> EditImage(string apiKey, string imageKey, string newImageTitle, string newImageDescription, string collectionName = "default");
    
    Task<OperationResultDto<bool>> DeleteImage(string apiKey, string imageKey, string collectionName = "default");

    Task<OperationResultDto<IEnumerable<ImageCollectionDto>>> ListCollections(string apiKey);
    
    Task<OperationResultDto<ImageCollection>> CreateCollection(string apiKey, string collectionName, string? description = default); 

    Task<OperationResultDto<bool>> EditCollection(string apiKey, string collectionName,string newCollectionName, string? newCollectionDescription = default); 
    
    Task<OperationResultDto<bool>> DeleteCollection(string apiKey, string collectionName);
}