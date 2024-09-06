using System.Runtime.InteropServices.JavaScript;
using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Mappers;
using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using DailyUsageMetrics = ImageVault.RequestMetricsService.Data.Dtos.DailyUsageMetrics;
using Request = ImageVault.RequestMetricsService.Data.Models.Request;

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
    
    public async Task<OperationResult<bool>> AddRequest(Data.Dtos.Request request)
    {
        if (request == null)
        {
            _logger.LogError("UserId in AddStorageUsage in DailyUsageMetricsRepository is empty. It shouldn't be empty");
            return new OperationResult<bool>(false,false,new Error("UserId in AddStorageUsage in DailyUsageMetricsRepository is empty. It shouldn't be empty")); 
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
            _logger.LogError("UserId in AddStorageUsage in DailyUsageMetricsRepository is empty. It shouldn't be empty ");
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
            _logger.LogError("UserId in AddStorageUsage in DailyUsageMetricsRepository is empty. It shouldn't be empty ");
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
            _logger.LogError("UserId in GetDailyUsageMetrics in DailyUsageMetricsRepository is empty. It shouldn't be empty ");
        }

        var dailyUsage = new Data.Models.DailyUsageMetrics()
        {
            UserId = userId, 
            Requests = new List<Request>(),
            TotalRequests = 0,
            TotalImageUploaded = 0,
            TotalStorageUsed = 0
        };

        await _dbContext.UsersDailyUsageMetrics.AddAsync(dailyUsage);

        await SaveChanges();

        return dailyUsage; 
    }


    private async Task<Data.Models.DailyUsageMetrics?> GetDailyUsageMetrics(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogError("UserId in GetDailyUsageMetrics in DailyUsageMetricsRepository is empty. It shouldn't be empty ");
            return null; 
        }
       
        var result = await _dbContext.UsersDailyUsageMetrics.Include(x => x.Requests).FirstOrDefaultAsync(x => x.UserId == userId && x.Date == DateTime.Today);

        return result == null
            ? await CreateDailyUsageMetrics(userId)
            : result;
    }
        
    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }
}