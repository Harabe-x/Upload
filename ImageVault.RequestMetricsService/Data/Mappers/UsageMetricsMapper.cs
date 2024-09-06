using System.Linq.Expressions;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Models;

namespace ImageVault.RequestMetricsService.Data.Mappers;

public static class UsageMetricsMapper
{
   public static UsageMetric MapToUsageMetrics(this UsageMetrics metrics)
   {
      return new UsageMetric(metrics.TotalRequests, metrics.TotalImageUploaded, metrics.TotalStorageUsed);
   }
   
   public static DailyUsageMetric MapToDailyUsageMetric(this DailyUsageMetrics metrics)
   {
      return new DailyUsageMetric(metrics.TotalRequests,metrics.TotalImageUploaded,metrics.TotalStorageUsed,metrics.Date);
   }

}


