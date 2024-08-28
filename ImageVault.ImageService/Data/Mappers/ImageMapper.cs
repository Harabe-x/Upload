using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ImageVault.ImageService.Data.Mappers;

/// <summary>
/// Image Mappers
/// </summary>
public static class ImageMapper
{
    /// <summary>
    /// Maps <see cref="Image"/> to <see cref="ImageDto"/>
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static ImageDto MapToImageDto(this Image image)
    {
        return new ImageDto(image.Key, image.Url, image.Collection, image.Title, image.Description, image.ImageFormat,image.CreatedAt);
    }

    /// <summary>
    ///  Maps <see cref="ImageDataDto"/> to <see cref="Image"/>
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static Image MapToImage(this ImageDataDto imageData ,ImageCollection collection)
    {
        return new Image
        {
            Collection = collection.CollectionName,
            ApiKey = imageData.ApiKey, 
            Title = imageData.Title, 
            Description = imageData.Description ,
            UserId = imageData.UserId,
            ImageCollectionId = collection.Id,
            Key = imageData.Key,
            ImageSize = imageData.ImageSize,
            ImageFormat = imageData.FileFormat,
            Url = "dckyy64qykbap.cloudfront.net/" + imageData.Key
        };
    }
}