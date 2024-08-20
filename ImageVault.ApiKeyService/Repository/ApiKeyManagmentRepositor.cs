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

namespace ImageVault.ApiKeyService.Repository;

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

    public async Task<OperationResultDto<ApiKeyDto>> AddKey(AddApiKeyDto apiKeyData, string userId)
    {
        if (!_validationService.ValidateData("ValidateKeyName", apiKeyData.KeyName) ||
            !_validationService.ValidateData("ValidateKeyCapacity", apiKeyData.KeyCapacity))
            return new OperationResultDto<ApiKeyDto>(null, false, new Error("Invalid key data"));

        var apiKey = apiKeyData.MapToApiKey(userId);

        await _dbContext.ApiKeys.AddAsync(apiKey);

        SendApiKeyCopyToImageService(apiKey.Key, apiKey.UserId, ApiKeyOperationType.Create);
        
        return await SaveChanges()
            ? new OperationResultDto<ApiKeyDto>(apiKey.MapToApiKeyDto(), true, null)
            : new OperationResultDto<ApiKeyDto>(null, false, new Error("Something went wrong ..."));
    }

    public async Task<OperationResultDto<IEnumerable<ApiKeyDto>>> GetKeys(string userId)
    {
        var result = await _dbContext.ApiKeys
            .Where(x => x.UserId == userId)
            .Select(x => x.MapToApiKeyDto())
            .ToListAsync();
        return new OperationResultDto<IEnumerable<ApiKeyDto>>(result, true, null);
    }

    public async Task<OperationResultDto<ApiKeyDto>> GetKey(string key, string userId)
    {
        var apiKey = await GetApiKey(key, userId);

        if (apiKey == null)
            return new OperationResultDto<ApiKeyDto>(null, false, new Error("Api key doesn't exists"));

        return new OperationResultDto<ApiKeyDto>(apiKey.MapToApiKeyDto(), true, null);
    }

    public async Task<OperationResultDto<ApiKeyDto>> EditKey(EditApiKeyDto newApiKeyData, string userId, string key)
    {
        if (!_validationService.ValidateData("ValidateKeyName", newApiKeyData.KeyName) ||
            !_validationService.ValidateData("ValidateKeyCapacity", newApiKeyData.KeyCapacity))
            return new OperationResultDto<ApiKeyDto>(null, false, new Error("Invalid key data"));

        var apiKey = await GetApiKey(key, userId);

        if (apiKey == null)
            return new OperationResultDto<ApiKeyDto>(null, false, new Error("Api key doesn't exists"));

        

        apiKey.KeyName = newApiKeyData.KeyName;
        apiKey.StorageCapacity = newApiKeyData.KeyCapacity;

        _dbContext.ApiKeys.Update(apiKey);

        return await SaveChanges()
            ? new OperationResultDto<ApiKeyDto>(apiKey.MapToApiKeyDto(), true, null)
            : new OperationResultDto<ApiKeyDto>(null, false, null);
    }

    public async Task<OperationResultDto<bool>> DeleteKey(string key, string userId)
    {
        var apiKey = await GetApiKey(key, userId);

        if (apiKey == null)
            return new OperationResultDto<bool>(false, false, new Error("Api key doesn't exists"));

        _dbContext.ApiKeys.Remove(apiKey);

        SendApiKeyCopyToImageService(apiKey.Key, apiKey.UserId, ApiKeyOperationType.Delete);
        
        return await SaveChanges()
            ? new OperationResultDto<bool>(true, true, null)
            : new OperationResultDto<bool>(false, false, null);
    }

    private async Task<bool> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
    
    private async Task<ApiKey> GetApiKey(string key, string userId)
    {
        return await _dbContext.ApiKeys.FirstOrDefaultAsync(x => x.Key == key && x.UserId == userId);
    }

    private void SendApiKeyCopyToImageService(string key, string userId, ApiKeyOperationType operationType, string? newKey = null)
    {
        var apiKeyTransferData = new ApiKeyTransferDataDto(key, userId, newKey, operationType);
        
        _messageSender.SendMessage(apiKeyTransferData, _configuration.GetApiKeyQueueName());
    }
}