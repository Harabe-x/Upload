namespace ImageVault.RequestMetricsService.Data.Models;

public class UserRequestMetrics
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public int TotalRequests { get; set; }

    public int RemainingRequests { get; set; }
    
    public UserRequestMetrics()
    {
        Id = Guid.NewGuid().ToString();
    }
}