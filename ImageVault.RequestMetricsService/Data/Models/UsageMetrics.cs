namespace ImageVault.RequestMetricsService.Data.Models;

public class UsageMetrics
{
    public string Id { get; set; }
    
    public string UserId { get; set; }
     
    public uint TotalImageUploaded { get; set; }
    
    public uint TotalStorageUsed { get; set; }

    public uint TotalRequests { get; set; }
    
    public ICollection<DailyUsageMetrics> DailyUsageMetrics { get; set; }
    
    public UsageMetrics(string userId)
    {
        Id = Guid.NewGuid().ToString();
    }
}