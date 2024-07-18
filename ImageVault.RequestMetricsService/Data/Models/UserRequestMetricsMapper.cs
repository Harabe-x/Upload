using ImageVault.RequestMetricsService.Data.Dtos;

namespace ImageVault.RequestMetricsService.Data.Models;

public static class UserRequestMetricsMapper
{

    public static UserRequestMetricsDto MapToUserRequestMetricsDto(this UserRequestMetrics metrics)
    {
        return new UserRequestMetricsDto(metrics.TotalRequests,metrics.RemainingRequests);
    }
}