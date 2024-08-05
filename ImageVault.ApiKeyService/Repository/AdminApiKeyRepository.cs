using System.Runtime.CompilerServices;
using ImageVault.ApiKeyService.Data;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.ApiKeyService.Repository;

public class AdminApiKeyRepository : IAdminApiKeyRepository
{

    private readonly ApplicationDbContext _dbContext; 

    public AdminApiKeyRepository(ApplicationDbContext dbContext )
    {
        _dbContext = dbContext; 
    }
    
    public async Task<ApiKey> GetApiKey(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            return null;

        return await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == apiKey);
    }
}