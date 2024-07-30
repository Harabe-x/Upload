using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Mappers;

namespace ImageVault.RequestMetricsService.Repository;

public class RequestRepository : IRequestRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<IRequestRepository> _logger;

    public RequestRepository(ApplicationDbContext dbContext, ILogger<IRequestRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> AddRequest(RequestDto requestData)
    {
        var request = requestData.MapRequestDtoToRequest();

        await _dbContext.Requests.AddAsync(request);

        return await SaveChanges();
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}