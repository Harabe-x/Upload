using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.RequestMetricsService.Repository;

public class DailyUsageMetricsRepository : IDailyUsageMetricsRepository
{

    private readonly ILogger<DailyUsageMetrics> _logger;
    
    private readonly ApplicationDbContext _dbContext;
    
    public DailyUsageMetricsRepository(ApplicationDbContext dbContext, ILogger<DailyUsageMetrics> logger)
    {
        _dbContext = dbContext;
        _logger = logger; 
    }
    
    public Task<OperationResult<bool>> AddRequest(Request request)
    {
        
    }

    public Task<OperationResult<bool>> IncrementTotalUploadedImages()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<bool>> AddStorageUsage(uint bytesUsed)
    {
        throw new NotImplementedException();
    }


    private async Task<Data.Models.DailyUsageMetrics?> CreateDailyUsageMetrics(string userId)
    { 
        throw new NotImplementedException();
    }


    private async Task<Data.Models.DailyUsageMetrics?> GetDailyUsageMetrics(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in GetDailyUsageMetrics in DailyUsageMetricsRepository is empty. It shouldn't be empty ");
            return null; 
        }
        
        var result = await _dbContext.UsersDailyUsageMetrics.FirstOrDefaultAsync(x => x.UserId == userId && x.Date == DateTime.Today);

        return result == null
            ? await CreateDailyUsageMetrics(userId)
            : result;
    }
        
    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }
}