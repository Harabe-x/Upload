namespace ImageVault.RequestMetricsService.Data.Dtos;

public record DailyUsageMetrics(int TotalRequests, int TotalImageUploaded, int TotalStorageUsed, DateTime Date); 