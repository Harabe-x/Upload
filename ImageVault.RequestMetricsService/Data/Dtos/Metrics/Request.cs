namespace ImageVault.RequestMetricsService.Data.Dtos;

public record Request(
    string UserId,
    DateTime TimeStamp,
    string Endpoint,
    string Ip,
    string Method
);