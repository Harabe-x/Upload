namespace ImageVault.ImageService.Data.Dtos.Collection;


/// <summary>
/// A record representing the data needed to delete a collection
/// </summary>
/// <param name="ApiKey"> The API key on the basis of which the collection will be found</param>
/// <param name="CollectionName">Name of collection</param>
public record DeleteCollectionDto(string ApiKey,string CollectionName); 