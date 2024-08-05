using ImageVault.ApiKeyService.Data.Models;

namespace ImageVault.ApiKeyService.Data.Interfaces.ApiKey;

public interface IAdminApiKeyRepository
{
    Task<Models.ApiKey> GetApiKey(string apiKey);
}