using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ImageVault.RequestMetricsService.Data.Models;

public class Requests
{
    public string Id { get; set; }

    public string UserId { get; set; }
    
    public DateTime TimeStamp { get; set; }
    
    public string Endpoint { get; set;  } 
}

