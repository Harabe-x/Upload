using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ImageVault.ImageService.Data.Mappers;

public static class ImageMapper
{
    public static ImageDto MapToImageDto(this Image image)
    {
        return new ImageDto(image.Key, image.Url, image.Collection, image.Title, image.Description, image.ImageFormat,image.CreatedAt);
    }

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
            Key = collection.ApiKey,
            ImageSize = imageData.ImageSize,
            ImageFormat = imageData.FileFormat,
            Url = "dckyy64qykbap.cloudfront.net/" + imageData.Key
        };
    }
}