namespace ImageVault.RequestMetricsService.Data.Dtos.Log;

public record AnonymousRequest(DateTime TimeStamp, string Endpoint, string Ip, string Method);