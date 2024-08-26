using ImageVault.ApiKeyService.Data.Enums;

namespace ImageVault.ApiKeyService.Data.Dtos;

/// <summary>
///  Represents API key data to be sent to ImageService via AMQP
/// </summary>
/// <param name="Key"> API key</param>
/// <param name="UserId">The ID of the user to whom the key belongs</param>
/// <param name="NewKey">New API key in case the key needs to be changed</param>
/// <param name="OperationType">Type of operation to be performed in the ImageService</param>
public record ApiKeyTransferData (string Key, string UserId,string NewKey, ApiKeyOperationType OperationType); 