namespace ImageVault.RequestMetricsService.Data.Models;

public class Request
{
    public Request()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public string UserId { get; set; }

    public string DailyUsageMetricsId { get; set; }
    
    public DailyUsageMetrics DailyUsageMetrics { get; set;  }
    
    public DateTime TimeStamp { get; set; }

    public string Endpoint { get; set; }

    public string Ip { get; set; }

    public string Method { get; set; }
}