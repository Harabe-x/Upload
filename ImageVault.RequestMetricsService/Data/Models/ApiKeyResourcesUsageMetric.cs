namespace ImageVault.RequestMetricsService.Data.Models;

public class ApiKeyResourcesUsageMetric
{
    public string Id { get; set; }
    
    public string UserId { get; set; }

    public string ApiKeyId { get; set; }
    
    public int ImageUploaded { get; set; }
    
    public double StorageUsed { get; set; }
    
    public double StorageAvailable { get; set; }
    
}