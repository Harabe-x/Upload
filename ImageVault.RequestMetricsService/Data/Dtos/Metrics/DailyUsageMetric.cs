namespace ImageVault.RequestMetricsService.Data.Dtos;

public record DailyUsageMetric(uint TotalRequests, uint TotalImageUploaded, uint TotalStorageUsed, DateTime Date); 