namespace ImageVault.RequestMetricsService.Data.Dtos;

public record UsageMetrics(int TotalRequests, int TotalImageUploaded, int TotalStorageUsed);