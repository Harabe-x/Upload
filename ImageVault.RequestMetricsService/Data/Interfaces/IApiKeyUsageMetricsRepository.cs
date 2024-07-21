using System.Runtime.CompilerServices;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IApiKeyUsageMetricsRepository
{

    public Task<bool> UpdateData(string userId, string apiKey, IFormFile file); 
    
    
}