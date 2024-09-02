namespace ImageVault.ImageService.Data.Dtos.Collection;


/// <summary>
///  A record representing the collection data that is returned to the user
/// </summary>
/// <param name="CollectionName">Name of the collection</param>
/// <param name="CollectionDescription">Description of the collection</param>
/// <param name="CollectionCoverUrl">Url to collection cover </param>
/// <param name="TotalImages">The total number of images contained in the collection</param>
public record ImageCollectionDto(string Id,string CollectionName, string CollectionDescription, string CollectionCoverUrl,uint TotalImages);