using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Models;

namespace ImageVault.ImageService.Data.Mappers;

public static class ImageMapper
{
    public static ImageDto MapToImageDto(this Image image)
    {
        return new ImageDto(image.Key, image.Key, image.Collection, image.Title, image.Description, image.CreatedAt);
    }
}