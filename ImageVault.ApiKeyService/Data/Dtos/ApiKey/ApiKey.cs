namespace ImageVault.ApiKeyService.Data.Dtos;


/// <summary>
///  A record representing the API key data to return to the user
/// </summary>
/// <param name="UserId"> The ID of the user to whom the key belongs  </param>
/// <param name="KeyName"> API key name</param>
/// <param name="Key"> API key</param>
/// <param name="StorageUsed"> Storage used by API key </param>
public record ApiKey
(
    string UserId,
    string KeyName,
    string Key,
    ulong StorageUsed
);