using ImageVault.RequestMetricsService.Data.Dtos;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IUsageMetricsRepository
{
    Task<OperationResult<UsageMetrics>> GetTotalUsageMetrics(string userId); 
    
    Task<OperationResult<UsageMetrics>> GetDailyUsageMetrics(string userId, int daysRange); 

}