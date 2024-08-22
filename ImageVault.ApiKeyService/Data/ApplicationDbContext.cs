using ImageVault.ApiKeyService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.ApiKeyService.Data;


/// <summary>
/// /// Class responsible for managing the database context of the application,
/// </summary>
public class ApplicationDbContext : DbContext

{
    /// <summary>
    ///  Constructor for ApplicationDbContext
    /// </summary>
    /// <param name="options">DbContext configuration options</param>

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    /// <summary>
    /// Representation of API keys table 
    /// </summary>
    public DbSet<ApiKey> ApiKeys { get; set; }
}   