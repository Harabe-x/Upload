namespace ImageVault.RequestMetricsService.Data.Models;

public class ApiKeyUsageMetric
{
    public string Id { get; set; }
    
    public string UserId { get; set; }

    public string ApiKey { get; set; }
    
    public int ImageUploaded { get; set; }
    
    public double StorageUsedGb { get; set; }
    
    public double StorageAvailableGb { get; set; }


    public ApiKeyUsageMetric()
    {
        Id = Guid.NewGuid().ToString();
    }
    
}