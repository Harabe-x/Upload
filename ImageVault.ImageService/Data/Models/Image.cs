using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImageVault.ImageService.Data.Models;

public class Image
{
    [Required] [Key] public string Id { get; set; }

    [Required] public string UserId { get; set; }

    [Required] public string Key { get; set; }
    
    [Required] public string ApiKey { get; set; }  
    
    public string? Collection { get; set; }

    public string? Title { get; set; }
    
    public string? Description { get; set; }

    public string ImageFormat { get; set; }
    
    public string? DownloadUrl { get; set; }
    
    public ulong ImageSize { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public string Url { get; set; }
    
    public string? ImageCollectionId { get; set; }

    [ForeignKey(nameof(ImageCollectionId))]
    public ImageCollection? ImageCollection { get; set; }

    public Image()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.Now;
    }
}