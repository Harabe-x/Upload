using ImageVault.UploadService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.UploadService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)  { }
    
    public DbSet<ApiKey> ApiKeys { get; set; }
 }