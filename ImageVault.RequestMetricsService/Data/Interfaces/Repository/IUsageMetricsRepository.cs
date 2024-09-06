using ImageVault.RequestMetricsService.Data.Dtos;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IUsageMetricsRepository
{
    Task<OperationResult<UsageMetric>> GetTotalUsageMetrics(string userId); 
    
    Task<OperationResult<IEnumerable<DailyUsageMetric>>> GetDailyUsageMetrics(string userId, int daysRange);
    

}