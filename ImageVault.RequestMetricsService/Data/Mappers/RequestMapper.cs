using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Models;
using DailyUsageMetrics = ImageVault.RequestMetricsService.Data.Models.DailyUsageMetrics;
using Request = ImageVault.RequestMetricsService.Data.Models.Request;

namespace ImageVault.RequestMetricsService.Data.Mappers;

public static class RequestMapper
{
    public static Request MapToRequestModel(this Dtos.Request request, DailyUsageMetrics usageMetrics)
    {
        return new Request
        {
            UserId = request.UserId,
            Endpoint = request.Endpoint,
            Method = request.Method,
            Ip = request.Ip,
            TimeStamp = request.TimeStamp.ToUniversalTime(),
            DailyUsageMetricsId = usageMetrics.Id,
            DailyUsageMetrics =usageMetrics,
        };
    }
}