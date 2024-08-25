using ImageVault.ImageService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.ImageService.Data;


/// <summary>
/// /// Class responsible for managing the database context of the application,
/// </summary>
public class ApplicationDbContext : DbContext
{

    /// <summary>
    ///  Constructor 
    /// </summary>
    /// <param name="options"></param>
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    
    /// <summary>
    /// Representation of Images table
    /// </summary>
    public DbSet<Image> Images { get; set; }
    
    /// <summary>
    /// Representation of ImageCollection Tables
    /// </summary>
    public DbSet<ImageCollection> ImageCollections { get; set; }
    
    /// <summary>
    /// Representation of API keys table
    /// </summary>
    public DbSet<ApiKey> ApiKeys { get; set; }
}