namespace ImageVault.UserService.Data.Dtos.ApiKey;

public record ApiKeyDto(string KeyName, string Key, decimal KeyStorageCapacity, DateTime CreatedAt);