namespace ImageVault.RequestMetricsService.Data.Models;

public class ApiKeyUsageMetric
{
    public ApiKeyUsageMetric()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public string UserId { get; set; }

    public string ApiKey { get; set; }

    public int TotalImages { get; set; }

    public ulong StorageUsed { get; set; }
}