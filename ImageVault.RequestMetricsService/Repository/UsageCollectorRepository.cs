using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.JavaScript;
using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Mappers;
using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using Request = ImageVault.RequestMetricsService.Data.Models.Request;
using UsageMetrics = ImageVault.RequestMetricsService.Data.Models.UsageMetrics;

namespace ImageVault.RequestMetricsService.Repository;

public class UsageCollectorRepository : IUsageCollectorRepository
{

    private readonly ILogger<UsageCollectorRepository> _logger;
    
    private readonly ApplicationDbContext _dbContext;
    
    public UsageCollectorRepository(ApplicationDbContext dbContext, ILogger<UsageCollectorRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger; 
    }
    
    public async Task<OperationResult<bool>> AddRequest(Data.Dtos.Request request)
    {
        if (request == null)
        {
            _logger.LogError("UserId in AddStorageUsage in UsageCollectorRepository is empty. It shouldn't be empty");
            return new OperationResult<bool>(false,false,new Error("UserId in AddStorageUsage in UsageCollectorRepository is empty. It shouldn't be empty")); 
        }

        var dailyUsage = await GetDailyUsageMetrics(request.UserId);


        var requestModel = request.MapToRequestModel(dailyUsage);

        dailyUsage.TotalRequests += 1; 
        dailyUsage.Requests.Add(requestModel);
        
        
        return await SaveChanges()
            ? new OperationResult<bool>(true, true, null)
            : new OperationResult<bool>(false,false, new Error("Something went wrong whiel adding request to the database."));


    }

    public async Task<OperationResult<bool>> IncrementTotalUploadedImages(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in AddStorageUsage in UsageCollectorRepository is empty. It shouldn't be empty ");
            return null; 
        }
        
        var todayDailyUsage = await GetDailyUsageMetrics(userId);

        todayDailyUsage.TotalImageUploaded += 1; 

        _dbContext.UsersDailyUsageMetrics.Update(todayDailyUsage);

        return await SaveChanges()
            ? new OperationResult<bool>(true, true, null)
            : new OperationResult<bool>(false, false, new Error("Something went wrong while updating daily data usage"));
    }

    public async Task<OperationResult<bool>> AddStorageUsage(uint bytesUsed,string userId)
    {
    
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in AddStorageUsage in UsageCollectorRepository is empty. It shouldn't be empty ");
            return null; 
        }
        
        var todayDailyUsage = await GetDailyUsageMetrics(userId);

        todayDailyUsage.TotalStorageUsed += bytesUsed;

         _dbContext.UsersDailyUsageMetrics.Update(todayDailyUsage);

         return await SaveChanges()
             ? new OperationResult<bool>(true, true, null)
             : new OperationResult<bool>(false, false, new Error("Something went wrong while updating daily data usage"));
    }


    private async Task<Data.Models.DailyUsageMetrics?> CreateDailyUsageMetrics(string userId)
    { 
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in GetDailyUsageMetrics in UsageCollectorRepository is empty. It shouldn't be empty ");
        }

        var usageMetrics = await GetUsageMetrics(userId);
        
        
        var dailyUsage = new Data.Models.DailyUsageMetrics()
        {
            UserId = userId, 
            Requests = new List<Request>(),
            TotalRequests = 0,
            TotalImageUploaded = 0,
            TotalStorageUsed = 0,
            Date = DateTime.Today
        };

        usageMetrics.DailyUsageMetrics.Add(dailyUsage);

        await SaveChanges();

        return dailyUsage; 
    }

    private async Task<UsageMetrics> CreateUsageMetrics(string userId)
    {
        
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in CreateUsageMetrics in UsageCollectorRepository is empty. It shouldn't be empty ");
            return null; 
        }

        var usageMetrics = new UsageMetrics()
        { 
            DailyUsageMetrics = new List<Data.Models.DailyUsageMetrics>(),
            TotalRequests = 0,
            TotalStorageUsed = 0,
            TotalImageUploaded = 0,
            UserId = userId,
        };

        await _dbContext.UserUsageMetrics.AddAsync(usageMetrics); 
        
        var result = await SaveChanges();

        if (!result)
        {
            _logger.LogError("Error occured while creating DailyUsage");
        }

        return usageMetrics;

    }

    private async Task<UsageMetrics> GetUsageMetrics(string userId)
    {
        
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in GetUsageMetrics in UsageCollectorRepository is empty. It shouldn't be empty ");
            return null; 
        }

        var usageMetrics = await _dbContext.UserUsageMetrics.Include(x => x.DailyUsageMetrics).FirstOrDefaultAsync(x => x.UserId == userId);

        if (usageMetrics == null)
        {
            usageMetrics = await CreateUsageMetrics(userId);
        }

        return usageMetrics;
    }


    private async Task<Data.Models.DailyUsageMetrics?> GetDailyUsageMetrics(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in GetDailyUsageMetrics in UsageCollectorRepository is empty. It shouldn't be empty ");
            return null; 
        }

        var usageMetrics = await GetUsageMetrics(userId);

        var result = _dbContext.UsersDailyUsageMetrics.Include(x => x.Requests).FirstOrDefault(x => x.UserId == userId && x.Date == DateTime.Today); 
        
        return result == null
            ? await CreateDailyUsageMetrics(userId)
            : result;
    }
        
    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }
}