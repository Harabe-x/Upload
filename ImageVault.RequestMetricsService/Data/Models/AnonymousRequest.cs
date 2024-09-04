namespace ImageVault.RequestMetricsService.Data.Models;

public class AnonymousRequest
{

    public AnonymousRequest()
    {
        Id = Guid.NewGuid().ToString(); 
    }
    
    public string Id { get; set; }
    
    public DateTime TimeStamp { get; set; }

    public string Endpoint { get; set; }

    public string Ip { get; set; }

    public string Method { get; set; }
}