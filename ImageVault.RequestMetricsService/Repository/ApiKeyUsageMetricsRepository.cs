using ImageVault.RequestMetricsService.Data.Interfaces;

namespace ImageVault.RequestMetricsService.Repository;

public class ApiKeyUsageMetricsRepository : IApiKeyUsageMetricsRepository
{
    public Task<bool> UpdateData(string userId, string apiKey, IFormFile file)
    {
        throw new NotImplementedException();
    }
}