using ImageVault.ApiKeyService.Data;
using ImageVault.ApiKeyService.Data.Dtos;
using ImageVault.ApiKeyService.Data.Enums;
using ImageVault.ApiKeyService.Data.Interfaces.ApiKey;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using ImageVault.ApiKeyService.Data.Interfaces.Services;
using ImageVault.ApiKeyService.Data.Mappers;
using ImageVault.ApiKeyService.Data.Models;
using ImageVault.ApiKeyService.Extension;
using Microsoft.EntityFrameworkCore;
using ApiKey = ImageVault.ApiKeyService.Data.Dtos.ApiKey;

namespace ImageVault.ApiKeyService.Repository;

/// <summary>
/// <inheritdoc cref="IApiKeyRepository"/>
/// </summary>
public class ApiKeyRepository : IApiKeyRepository
{
    private readonly ApplicationDbContext _dbContext;

    private readonly ILogger<ApiKeyRepository> _logger;

    private readonly IDataValidationService _validationService;

    private readonly IRabbitMqMessageSender _messageSender;

    private readonly IConfiguration _configuration; 

    public ApiKeyRepository(ApplicationDbContext dbContext, IDataValidationService validationService,
        ILogger<ApiKeyRepository> logger,IRabbitMqMessageSender messageSender, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _validationService = validationService;
        _logger = logger;
        _messageSender = messageSender;
        _configuration = configuration; 
    }

    public async Task<OperationResult<ApiKey>> AddKey(AddApiKey apiKeyData, string userId)
    {
        if (!_validationService.ValidateData("ValidateKeyName", apiKeyData.KeyName) ||
            !_validationService.ValidateData("ValidateKeyCapacity", apiKeyData.KeyCapacity))
            return new OperationResult<ApiKey>(null, false, new Error("Invalid key data"));

        var apiKey = apiKeyData.MapToApiKey(userId);
        
        await _dbContext.ApiKeys.AddAsync(apiKey);

        SendApiKeyCopyToImageService(apiKey.Key, apiKey.UserId, ApiKeyOperationType.Create);
        
        return await SaveChanges()
            ? new OperationResult<ApiKey>(apiKey.MapToApiKeyDto(), true, null)
            : new OperationResult<ApiKey>(null, false, new Error("Something went wrong ..."));
    }

    public async Task<OperationResult<IEnumerable<ApiKey>>> GetKeys(string userId)
    {

        if (string.IsNullOrWhiteSpace(userId))
            return new OperationResult<IEnumerable<ApiKey>>(null , false, new Error("Something went wrong. Please try again later."));
        
        var result = await _dbContext.ApiKeys
            .Where(x => x.UserId == userId)
            .Select(x => x.MapToApiKeyDto())
            .ToListAsync();
        return new OperationResult<IEnumerable<ApiKey>>(result, true, null);
    }

    public async Task<OperationResult<ApiKey>> GetKey(string key, string userId)
    {

        if (string.IsNullOrWhiteSpace())
            return new OperationResult<ApiKey>(null, false, new Error("Provided key was empty")); 
        var apiKey = await GetApiKey(key, userId);

        if (apiKey == null)
            return new OperationResult<ApiKey>(null, false, new Error("Api key doesn't exists"));

        return new OperationResult<ApiKey>(apiKey.MapToApiKeyDto(), true, null);
    }

    public async Task<OperationResult<ApiKey>> EditKey(EditApiKey newApiKeyData, string userId, string key)
    {
        if (!_validationService.ValidateData("ValidateKeyName", newApiKeyData.KeyName) ||
            !_validationService.ValidateData("ValidateKeyCapacity", newApiKeyData.KeyCapacity))
            return new OperationResult<ApiKey>(null, false, new Error("Invalid key data"));

        var apiKey = await GetApiKey(key, userId);

        if (apiKey == null)
            return new OperationResult<ApiKey>(null, false, new Error("Api key doesn't exists"));

        apiKey.KeyName = newApiKeyData.KeyName;
        apiKey.StorageCapacity = newApiKeyData.KeyCapacity;

        _dbContext.ApiKeys.Update(apiKey);

        return await SaveChanges()
            ? new OperationResult<ApiKey>(apiKey.MapToApiKeyDto(), true, null)
            : new OperationResult<ApiKey>(null, false, null);
    }

    public async Task<OperationResult<bool>> DeleteKey(string key, string userId)
    {
        var apiKey = await GetApiKey(key, userId);

        if (apiKey == null)
            return new OperationResult<bool>(false, false, new Error("Api key doesn't exists"));

        _dbContext.ApiKeys.Remove(apiKey);

        SendApiKeyCopyToImageService(apiKey.Key, apiKey.UserId, ApiKeyOperationType.Delete);
        
        return await SaveChanges()
            ? new OperationResult<bool>(true, true, null)
            : new OperationResult<bool>(false, false, null);
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
    
    private async Task<Data.Models.ApiKey> GetApiKey(string key, string userId)
    {
        return await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == key && x.UserId == userId);
    }

    private void SendApiKeyCopyToImageService(string key, string userId, ApiKeyOperationType operationType, string? newKey = null)
    {
        var apiKeyTransferData = new ApiKeyTransferData(key, userId, newKey, operationType);
        
        _messageSender.SendMessage(apiKeyTransferData, _configuration.GetApiKeyQueueName());
    }
}