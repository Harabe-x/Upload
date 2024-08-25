using ImageVault.ImageService.Data.Dtos.Collection;
using ImageVault.ImageService.Data.Models;

namespace ImageVault.ImageService.Data.Mappers;

/// <summary>
/// Collection Mapper
/// </summary>
public static class CollectionMapper
{
    /// <summary>
    ///  Mapps <see cref="ImageCollectionDto"/> To <see cref="ImageCollection"/>
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static ImageCollectionDto MapToCollectionDto(this ImageCollection collection)
    {
        return new ImageCollectionDto(collection.CollectionName, collection.Description, collection.CollectionCoverUrl, collection.TotalImages);
    }
}