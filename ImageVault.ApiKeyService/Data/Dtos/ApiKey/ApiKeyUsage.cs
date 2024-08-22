namespace ImageVault.ApiKeyService.Data.Dtos;


/// <summary>
///   A record representing the data that UploadService needs to upload the photo
/// </summary>
/// <param name="userId">The ID of the user to whom the key belongs </param>
/// <param name="usedData"> Data that is used by the API key</param>
/// <param name="key">API key </param>
public record ApiKeyUsage(string userId, ulong usedData, string key);