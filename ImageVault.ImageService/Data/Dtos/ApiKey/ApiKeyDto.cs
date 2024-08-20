using ImageVault.ImageService.Data.Enums;

namespace ImageVault.ImageService.Data.Dtos.ApiKey;

public record ApiKeyDto (string ApiKey, string UserId,string NewKey, ApiKeyOperationType OperationType); 