using System.Runtime.CompilerServices;
using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Mappers;
using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ImageVault.RequestMetricsService.Repository;

public class ApiKeyLogsRepository : IApiKeyLogsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ApiKeyLogsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    
    public async Task<OperationResult<IEnumerable<Data.Dtos.Log.ApiKeyLog>>> GetLogs(string apiKey, int limit, int page)
    {
        var apiKeyLogs = await GetApiKeyLogs(apiKey); 
        var pageToSkip = (page - 1) * limit;

        var result = apiKeyLogs.Logs.Skip(pageToSkip).Take(limit).Select(x => x.MapToApiKeyLog());

        return new OperationResult<IEnumerable<Data.Dtos.Log.ApiKeyLog>>(result, true, null);
    }

    public async Task<OperationResult<bool>> AddLog(string apiKey, string message)
    {
        var apiKeyLogs = await GetApiKeyLogs(apiKey);

        var apiKeyLog = new ApiKeyLog()
        {
            Message = message, 
            TimeStamp = DateTime.Now.ToUniversalTime(),
            ApiKeysLogId = apiKeyLogs.Id
        };
        
        apiKeyLogs.Logs.Add(apiKeyLog);

        var result = await SaveChanges();

        return result
            ? new OperationResult<bool>(true, true, null)
            : new OperationResult<bool>(false, false, new Error("Something went wrong when saving logs\""));
    }

    private async Task<ApiKeyLogs> CreateApiKeyLogs(string apiKey)
    {
        var apiKeyLogs = new ApiKeyLogs()
        {
            ApiKey = apiKey,
            Logs = new List<ApiKeyLog>()
        };

        await _dbContext.ApiKeyLogs.AddAsync(apiKeyLogs);

        return apiKeyLogs;
    }

    private async Task<ApiKeyLogs> GetApiKeyLogs(string apiKey)
    {
        var apiKeysLogs = await _dbContext.ApiKeyLogs.Include(x => x.Logs).FirstOrDefaultAsync(x => x.ApiKey == apiKey);

        return apiKeysLogs == null
            ? await CreateApiKeyLogs(apiKey)
            : apiKeysLogs;
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

}