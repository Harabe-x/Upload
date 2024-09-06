namespace ImageVault.RequestMetricsService.Data.Models;

public class ApiKeyLog
{
    public string Id { get; set; }
    
    public string Message { get; set; }
    
    public DateTime TimeStamp { get; set; }
    
    public string ApiKeysLogId { get; set; }

    public ApiKeyLog()
    {
        Id = Guid.NewGuid().ToString();
    }
}