using ImageVault.RequestMetricsService.Data.Dtos.Log;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IAnonymousRequestRepository
{
    Task<bool> AddAnonymousRequest(AnonymousRequest request); 
}