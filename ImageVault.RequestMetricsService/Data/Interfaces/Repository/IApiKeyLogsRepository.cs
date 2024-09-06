using ImageVault.RequestMetricsService.Data.Dtos;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IApiKeyLogsRepository
{
    Task<OperationResult<IEnumerable<Data.Dtos.Log.ApiKeyLog>>> GetLogs(string apiKey,int limit,int page);

    Task<OperationResult<bool>> AddLog(string apiKey, string message);
}