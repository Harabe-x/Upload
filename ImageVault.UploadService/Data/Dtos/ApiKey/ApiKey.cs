namespace ImageVault.UploadService.Data.Dtos.ApiKey;

public record ApiKeyDto(
    string id,
    string userId,
    string keyName,
    string key,
    ulong storageCapacity,
    ulong storageUsed
);