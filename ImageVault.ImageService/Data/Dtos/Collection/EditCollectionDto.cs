namespace ImageVault.ImageService.Data.Dtos.Collection;

/// <summary>
///  A record representing the data needed to edit a collection
/// </summary>
/// <param name="ApiKey">The API key on the basis of which the collection will be found</param>
/// <param name="CollectionName">Name of collection to be edited</param>
/// <param name="NewCollectionName">New collection name</param>
/// <param name="NewCollectionDescription">New collection Description</param>
public record EditCollectionDto(string ApiKey, string CollectionName, string NewCollectionName, string NewCollectionDescription) ;