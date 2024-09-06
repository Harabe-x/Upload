namespace ImageVault.RequestMetricsService.Data.Models;

public class ApiKeyLogs
{
    public string Id { get; set; }
     
    public string ApiKey { get; set; }

    public ICollection<ApiKeyLog> Logs { get; set; }

    public ApiKeyLogs()
    {
        Id = Guid.NewGuid().ToString();
    }
}