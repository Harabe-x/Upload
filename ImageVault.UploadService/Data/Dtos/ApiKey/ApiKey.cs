// ReSharper disable InconsistentNaming
namespace ImageVault.UploadService.Data.Dtos.ApiKey;



/// <summary>
/// a record representing the API key
/// </summary>
/// <param name="id"></param>
/// <param name="userId"></param>
/// <param name="keyName"></param>
/// <param name="key"></param>
/// <param name="storageCapacity"></param>
/// <param name="storageUsed"></param>
/// <remarks>
/// This record is used to verify the API Key
/// </remarks>
public record ApiKey(
    string id,
    string userId,
    string keyName,
    string key,
    ulong storageCapacity,
    ulong storageUsed
);