using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.RequestMetricsService.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Requests> Requests { get; set;  }
    
    public DbSet<UserRequestMetrics> UsersRequestMetrics { get; set; }
    
    public DbSet<ApiKeyResourcesUsageMetric> ApiKeyResourcesUsageMetrics { get; set; }
    
}