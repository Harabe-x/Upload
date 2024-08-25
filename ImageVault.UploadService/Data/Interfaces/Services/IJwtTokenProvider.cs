namespace ImageVault.UploadService.Data.Interfaces.Services;


/// <summary>
/// Interface for providing JWT tokens used to authenticate with other services.
/// </summary>
public interface IJwtTokenProvider
{
    /// <summary>
    /// Gets or sets the JWT token used for authentication.
    /// </summary>
    string Token { get; set; }
}