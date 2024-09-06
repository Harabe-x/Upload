using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Models;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IApiKeyLogsRepository
{
    Task<OperationResult<IEnumerable<ApiKeyLog>>> GetLogs(string apiKey,string limit, string page);

    Task<OperationResult<bool>> AddLog(string apiKey, string message);
}