namespace ImageVault.ApiKeyService.Data.Dtos;

/// <summary>
///  A record representing a data about API key to be edited  
/// </summary>
/// <param name="KeyName">New API key name</param>
/// <param name="Key"> API key</param>
public record EditApiKey(string KeyName, string Key);