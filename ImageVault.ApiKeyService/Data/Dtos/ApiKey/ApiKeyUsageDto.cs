namespace ImageVault.ApiKeyService.Data.Dtos;

public record ApiKeyUsageDto(string userId, ulong usedData, string key ); 