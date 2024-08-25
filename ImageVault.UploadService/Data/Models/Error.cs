namespace ImageVault.UploadService.Data.Models;


/// <summary>
///  Record that represents error returned to user
/// </summary>
/// <param name="Message">Error Message</param>
public record Error(string Message);