namespace ImageVault.AuthenticationService.Data.Models;


/// <summary>
///  Record that represents error returned to user
/// </summary>
/// <param name="Message">Error message</param>
public record Error(string Message);