using ImageVault.UploadService.Data.Models;

namespace ImageVault.UploadService.Data.Dtos;

/// <summary>
///     A record that represents the status of the operation.
/// </summary>
/// <param name="Value"> Value is <typeparamref name="T" />  </param>
/// <param name="IsSuccess"> A flag identifying whether the operation is successful </param>
/// <param name="Error"> An object representing error </param>
/// <typeparam name="T"> Type of value</typeparam>
public record OperationResultDto<T>(T Value, bool IsSuccess, Error Error);