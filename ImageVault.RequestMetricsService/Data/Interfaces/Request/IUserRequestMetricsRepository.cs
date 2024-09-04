using ImageVault.RequestMetricsService.Data.Dtos;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IUserRequestMetricsRepository
{
    public Task<bool> UpdateUserStatstics(Request request);

    public Task<UserRequestMetrics> GetRequestMetrics(string userId);
}