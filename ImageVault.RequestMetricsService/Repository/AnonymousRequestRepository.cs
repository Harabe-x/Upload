using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos.Log;
using ImageVault.RequestMetricsService.Data.Interfaces;

namespace ImageVault.RequestMetricsService.Repository;

public class AnonymousRequestRepository : IAnonymousRequestRepository
{

    private readonly ApplicationDbContext _dbContext; 

    public AnonymousRequestRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; 
    }
    
    public async Task<bool> AddAnonymousRequest(AnonymousRequest request)
    {
        var anonymousRequest = new Data.Models.AnonymousRequest()
        {
            Ip = request.Ip,
            Endpoint = request.Endpoint,
            Method = request.Method,
            TimeStamp = request.TimeStamp
        };

        await _dbContext.AnonymousRequests.AddAsync(anonymousRequest);

        return await SaveChanges();
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0; 
    }
    
    
}