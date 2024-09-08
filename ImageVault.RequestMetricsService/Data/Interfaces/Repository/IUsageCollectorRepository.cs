using ImageVault.RequestMetricsService.Data.Dtos;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace ImageVault.RequestMetricsService.Data.Interfaces;

public interface IUsageCollectorRepository
{
    Task<OperationResult<bool>> AddRequest(Request request);

    Task<OperationResult<bool>> IncrementTotalUploadedImages(string userId);

    Task<OperationResult<bool>> IncrementTotalRequests(string userId); 

    Task<OperationResult<bool>> AddStorageUsage(uint bytesUsed, string userId);
}