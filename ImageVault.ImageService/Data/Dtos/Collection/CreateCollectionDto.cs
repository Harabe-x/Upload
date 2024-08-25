namespace ImageVault.ImageService.Data.Dtos.Collection;


/// <summary>
///  \A record representing the collection of images to be created
/// </summary>
/// <param name="ApiKey">API key associated with collection</param>
/// <param name="CollectionName"> Name of collection to be created</param>
/// <param name="CollectionDescription">Description of the collection to be created</param>
public record CreateCollectionDto(string ApiKey, string CollectionName, string CollectionDescription);