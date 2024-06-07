namespace ImageVault.Server.Models;

public class Image
{
    public string Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public Uri DownloadUrl { get; set; }

    public Uri Url { get; set; }
    
}