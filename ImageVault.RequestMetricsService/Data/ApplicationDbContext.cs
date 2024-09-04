using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.RequestMetricsService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Requests> Requests { get; set; }

    public DbSet<AnonymousRequest> AnonymousRequests { get; set; }
    
    public DbSet<DailyUsageMetrics> UsersDailyUsageMetrics { get; set; }
    
    public DbSet<UsageMetrics> UserUsageMetrics { get; set; }
}