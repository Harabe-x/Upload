using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.ImageService.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Image> Images { get; set; }
    
    public DbSet<ImageCollection> ImageCollections { get; set; }
    
    public DbSet<ApiKey> ApiKeys { get; set; }
}