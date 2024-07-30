namespace ImageVault.RequestMetricsService.Data.Models;

public class Requests
{
    public Requests()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public string UserId { get; set; }

    public DateTime TimeStamp { get; set; }

    public string Endpoint { get; set; }

    public string Ip { get; set; }

    public string Method { get; set; }
}