using System.ComponentModel.DataAnnotations;
using Azure.Core.Pipeline;

namespace ImageVault.ImageService.Data.Models;

public class ImageCollection
{
    [Required] [Key] public string Id { get; set; }
    
    [Required] public string CollectionName { get; set; }
    
    [Required] public string ApiKey { get; set; }
    
    public string Description { get; set; }
    
    
    public DateTime CreatedAt { get; set; }
    
    public string CollectionCoverUrl { get; set; }

    public ICollection<Image> ImagesCollection { get; set; }

    public ImageCollection()
    {
        Id = Guid.NewGuid().ToString(); 
    }
    
}