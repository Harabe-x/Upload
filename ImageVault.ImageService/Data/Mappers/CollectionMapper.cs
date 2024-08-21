using ImageVault.ImageService.Data.Dtos.Collection;
using ImageVault.ImageService.Data.Models;

namespace ImageVault.ImageService.Data.Mappers;

public static class CollectionMapper
{
    public static ImageCollectionDto MapToCollectionDto(this ImageCollection collection)
    {
        return new ImageCollectionDto(collection.CollectionName, collection.Description, collection.CollectionCoverUrl, collection.TotalImages);
    }
}