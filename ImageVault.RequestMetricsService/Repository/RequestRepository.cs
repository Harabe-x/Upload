using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Mappers;

namespace ImageVault.RequestMetricsService.Repository;

public class RequestRepository : IRequestRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<IRequestRepository> _logger;

    private readonly IUserRequestMetricsRepository _metricsRepository; 

    public RequestRepository(ApplicationDbContext dbContext, ILogger<IRequestRepository> logger,IUserRequestMetricsRepository metricsRepository)
    {
        _dbContext = dbContext;
        _logger = logger;
        _metricsRepository = metricsRepository; 
    }

    public async Task<bool> AddRequest(Request requestData)
    {
        var request = requestData.MapRequestDtoToRequest();

        await _dbContext.Requests.AddAsync(request);

        // even if user is unauthenticated it should count statistics for anon requests
        await _metricsRepository.UpdateUserStatstics(requestData);
        
        return await SaveChanges();
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}