using System.ComponentModel.DataAnnotations;

namespace ImageVault.ImageService.Data.Models;

public class ImageCollection
{
    [Required] [Key] public string Id { get; set; }
    
    [Required] public string CollectionName { get; set; }

    public string ApiKey { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public string CollectionCoverUrl { get; set; }

    public ICollection<Image> CollectionImages { get; set; }

    public ImageCollection()
    {
        Id = Guid.NewGuid().ToString(); 
    }
    
}