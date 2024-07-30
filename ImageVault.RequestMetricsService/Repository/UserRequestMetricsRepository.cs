using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.RequestMetricsService.Repository;

public class UserRequestMetricsRepository : IUserRequestMetricsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRequestMetricsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> UpdateUserStatstics(RequestDto request)
    {
        var userMetrics = await _dbContext.UsersRequestMetrics.FirstOrDefaultAsync(x => x.UserId == request.UserId);

        if (userMetrics is not { })
        {
            userMetrics = await CreateUserMetrics(request.UserId);

            if (userMetrics is not { }) return false;
        }

        userMetrics.TotalRequests += 1;

        _dbContext.UsersRequestMetrics.Update(userMetrics);

        return await SaveChanges();
    }

    public async Task<UserRequestMetricsDto> GetRequestMetrics(string userId)
    {
        var request = await _dbContext.UsersRequestMetrics.FirstOrDefaultAsync(x => x.UserId == userId);

        if (request is not { }) return null;

        return request.MapToUserRequestMetricsDto();
    }

    private async Task<UserRequestMetrics> CreateUserMetrics(string userId)
    {
        var userMetrics = new UserRequestMetrics
        {
            UserId = userId
        };

        await _dbContext.AddAsync(userMetrics);

        await SaveChanges();

        return userMetrics;
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}