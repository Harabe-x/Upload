using ImageVault.ImageService.Data.Enums;

namespace ImageVault.ImageService.Data.Dtos.ApiKey;

/// <summary>
///  A record representing API key
/// </summary>
/// <param name="Key"> API Key </param>
/// <param name="UserId"></param>
/// <param name="NewKey">New API key</param>
/// <param name="OperationType">Operation to perform.</param>
/// <remarks>
///   This record is only used to verify that the specified API key actually exists in the API Key service
/// </remarks>
public record ApiKey (string Key, string UserId,string NewKey, ApiKeyOperationType OperationType); 