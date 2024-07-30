namespace ImageVault.RequestMetricsService.Data.Dtos;

public record RequestDto(
    string UserId,
    DateTime TimeStamp,
    string Endpoint,
    string Ip,
    string Method
);