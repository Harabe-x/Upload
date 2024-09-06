namespace ImageVault.RequestMetricsService.Data.Dtos;

public record UsageMetric(uint TotalRequests, uint TotalImageUploaded, uint TotalStorageUsed);