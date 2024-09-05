using System.Numerics;

namespace ImageVault.RequestMetricsService.Data.Models;

public class DailyUsageMetrics
{
    public string Id { get; set; }
    
    public string UserId { get; set; }
     
    public uint TotalImageUploaded { get; set; }
    
    public uint TotalStorageUsed { get; set; }

    public uint TotalRequests { get; set; }
    
    public DateTime Date { get; set; }
    
    public string UsageMetricsId { get; set; }

    public UsageMetrics UsageMetrics { get; set; }
    
    public ICollection<Request> Requests { get; set; }

    public DailyUsageMetrics()
    {
        Id = Guid.NewGuid().ToString(); 
        Date = DateTime.Today;
    }
}