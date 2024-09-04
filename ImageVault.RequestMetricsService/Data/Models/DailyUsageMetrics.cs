using System.Numerics;

namespace ImageVault.RequestMetricsService.Data.Models;

public class UserDailyResourceUsage
{
    public string Id { get; set; }
    
    public string UserId { get; set; }
     
    public uint TotalImageUploaded { get; set; }
    
    public uint TotalStorageUsed { get; set; }

    public uint TotalRequests { get; set; }
    
    public DateTime Date { get; set; }

    public UserDailyResourceUsage()
    {
        Id = Guid.NewGuid().ToString(); 
        Date = DateTime.Today;
    }
}