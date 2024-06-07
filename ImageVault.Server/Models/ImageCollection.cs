namespace ImageVault.Server.Models;

public class ImageCollection
{
    public string Id { get; set; }

    public string CollectionName { get; set; }
    
    public string CollectionDescription { get; set;  }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
     
    public Uri CollectionCoverUrl { get; set; }

    public IEnumerable<Image> CollectionImages { get; set; }
    
}