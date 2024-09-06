using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.RequestMetricsService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Request> Requests { get; set; }

    public DbSet<AnonymousRequest> AnonymousRequests { get; set; }
    
    public DbSet<DailyUsageMetrics> UsersDailyUsageMetrics { get; set; }
    
    public DbSet<UsageMetrics> UserUsageMetrics { get; set; }
    
    public DbSet<ApiKeyLogs> ApiKeyLogs { get; set; }
    
    public DbSet<ApiKeyLog> ApiKeyLogList { get; set; }
}