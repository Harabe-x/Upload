using ImageVault.RequestMetricsService.Data.Dtos;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IRequestRepository
{
    Task<bool> AddRequest(Request request);
}