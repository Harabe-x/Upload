using ImageVault.RequestMetricsService.Data.Dtos;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IUserRequestMetricsRepository
{
    public Task<bool> UpdateUserStatstics(RequestDto request);

    public Task<UserRequestMetricsDto> GetRequestMetrics(string userId); 
}