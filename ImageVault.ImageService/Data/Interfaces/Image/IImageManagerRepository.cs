using System.Runtime.CompilerServices;
using ImageVault.ImageService.Data.Dtos;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using HostingEnvironmentExtensions = Microsoft.Extensions.Hosting.HostingEnvironmentExtensions;

namespace ImageVault.ImageService.Data.Interfaces.Image;

public interface IImageManagerRepository
{
    Task<OperationResultDto<bool>> AddImage(string imageKey, string apiKey, string title, string description, string collectionName = "default");
    
    Task<OperationResultDto<ImageDto>> GetImage(string imageKey,string apiKey, string collectionName = "default");
    
    Task<OperationResultDto<ImageDto>> GetImages(string apiKey,string collectionName = "default");

    Task<OperationResultDto<bool>> DeleteImage(string apiKey, string imageKey, string collectionName = "default");
    
    Task<OperationResultDto<bool>> EditImage(string apiKey, string imageKey, string newImageTitle, string newImageDescription, string collectionName = "default");
    
    Task<OperationResultDto<ImageCollection>> CreateCollection(string apiKey, string collectionName, string? description = default); 

    Task<OperationResultDto<bool>> EditCollection(string apiKey, string collectionName, string? description = default); 
    
    Task<OperationResultDto<bool>> DeleteCollection(string apiKey, string collectionName);
}