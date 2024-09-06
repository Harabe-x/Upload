using System.Runtime.Intrinsics.X86;
using ImageVault.RequestMetricsService.Data.Models;

namespace ImageVault.RequestMetricsService.Data.Mappers;

public static class ApiKeyMapper
{
    public static Data.Dtos.Log.ApiKeyLog MapToApiKeyLog(this Data.Models.ApiKeyLog apiKeyLog)
    {
        return new Data.Dtos.Log.ApiKeyLog(apiKeyLog.Message, apiKeyLog.TimeStamp); 
    }
} 