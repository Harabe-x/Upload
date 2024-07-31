namespace ImageVault.ApiKeyService.Data.Dtos.Request;

public record RequestDto
(
    string UserId,
    DateTime TimeStamp,
    string Endpoint,
    string Ip,
    string Method
);