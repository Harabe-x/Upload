namespace ImageVault.ApiKeyService.Data.Dtos;


/// <summary>
///   A record representing the data that is needed to add a new API key
/// </summary>
/// <param name="KeyName">The name of the API key to be added</param>
/// <param name="KeyCapacity">The maximum amount bytes of data an API key can use </param>
public record AddApiKey(string KeyName, uint KeyCapacity);