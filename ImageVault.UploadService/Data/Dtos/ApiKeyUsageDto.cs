// ReSharper disable InconsistentNaming
namespace ImageVault.UploadService.Data.Dtos;

/// <summary>
///  Record representing apikey usage data sent to API key service via amqp 
/// </summary>
/// <param name="userId">The unique identifier for the user</param>
/// <param name="usedData"> the amount of data consumed by the user</param>
/// <param name="key">API key to which consumption data is to be assigned </param>
public record ApiKeyUsageDto(string userId, ulong usedData, string key);