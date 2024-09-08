using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Mappers;
using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace ImageVault.RequestMetricsService.Repository;

public class UsageMetricsRepository : IUsageMetricsRepository
{
    private readonly ILogger<UsageMetricsRepository> _logger;

    private readonly ApplicationDbContext _dbContext; 
    
    public UsageMetricsRepository(ApplicationDbContext dbContext, ILogger<UsageMetricsRepository> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<OperationResult<UsageMetric>> GetTotalUsageMetrics(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId is null or empty,  GetTotalUsageMetrics in UsageMetricsRepository.");
            return new OperationResult<UsageMetric>(null, false, new Error("UserId was empty"));
        }

        var metric = await GetUsageMetric(userId);

        var aggregationResult = await AggregateUsageData(metric);

        if (!aggregationResult)
        {
            _logger.LogError("Failed to aggregate user data");
        }
        
        return new OperationResult<UsageMetric>(metric.MapToUsageMetrics(), true, null);
    }

    public async Task<OperationResult<IEnumerable<DailyUsageMetric>>> GetDailyUsageMetrics(string userId, int daysRange)
    {
        if (daysRange <= 0)
            return new OperationResult<IEnumerable<DailyUsageMetric>>(null, false, new Error("Day range must be greater than zero "));
        
        var usageMetric = await GetUsageMetric(userId);

        if (usageMetric == null)
            return new OperationResult<IEnumerable<DailyUsageMetric>>(new List<DailyUsageMetric>(), true, null);

        var result = usageMetric.DailyUsageMetrics
            .OrderByDescending(x => x.Date)
            .Take(daysRange)
            .OrderBy(x => x.Date)
            .Select(x => x.MapToDailyUsageMetric());
        
        return new OperationResult<IEnumerable<DailyUsageMetric>>(result, true, null); 
    }

    private async Task<UsageMetrics> GetUsageMetric(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId is null or empty CreateUsageMetrics in GetUsageMetricsRepository");
            return null; 
        }
        
        var usageMetric = await _dbContext.UserUsageMetrics.Include(x => x.DailyUsageMetrics)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        return usageMetric; 
    }

    private async Task<UsageMetrics> CreateUsageMetrics(string userId)
    {

        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId is null or empty CreateUsageMetrics in UsageMetricsRepository.");
        }
        
        var metrics = await _dbContext.UserUsageMetrics.FirstOrDefaultAsync(x => x.UserId == userId);

        if (metrics != null) return metrics;

        var newMetrics = new UsageMetrics()
        {   
            TotalRequests = 0,
            TotalImageUploaded = 0, 
            TotalStorageUsed = 0,
            UserId = userId 
        };

        await _dbContext.UserUsageMetrics.AddAsync(newMetrics);
        
        var result = await SaveChanges();

        if (!result)
        {
            _logger.LogError("UsageMetrics creation failed.");
        }

        return newMetrics;
    }

    private async Task<bool> AggregateUsageData(UsageMetrics usageMetrics)
    {
        
        //  i disabled it 
        // if (usageMetrics.LastAggregationDate == DateTime.Today)
        // {
        //     _logger.LogInformation("Data was already aggregated today.");
        //     return true;
        // }

        uint totalUploadedImages = 0;
        uint totalStorageUsed = 0;
        uint totalRequests = 0;

        foreach (var metric in usageMetrics.DailyUsageMetrics)
        {
            totalUploadedImages += metric.TotalImageUploaded;
            totalStorageUsed += metric.TotalStorageUsed;
            totalRequests += metric.TotalRequests;
        }

        usageMetrics.TotalRequests = totalRequests;
        usageMetrics.TotalStorageUsed = totalStorageUsed;
        usageMetrics.TotalImageUploaded = totalUploadedImages;
        usageMetrics.LastAggregationDate = DateTime.Today;

        _dbContext.UserUsageMetrics.Update(usageMetrics);

        return await SaveChanges();
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }
}