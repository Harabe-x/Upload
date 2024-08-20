using ImageVault.ApiKeyService.Data.Enums;

namespace ImageVault.ApiKeyService.Data.Dtos;


public record ApiKeyTransferDataDto (string ApiKey, string UserId,string NewKey, ApiKeyOperationType OperationType); 