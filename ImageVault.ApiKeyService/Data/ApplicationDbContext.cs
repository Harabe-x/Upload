using ImageVault.ApiKeyService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.ApiKeyService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ApiKey> ApiKeys { get; set; }
}